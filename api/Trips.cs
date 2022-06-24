using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using api.Data;
using HITW.Function.Helpers;
using System.Linq;
using System.Security.Claims;
using System.Net;
using api.Data.Models;
using Flurl.Http;

namespace HITW.Function
{
    public class ClimatiqResp
    {
        [JsonProperty("co2e")]
        public double Co2e { get; set; }
    }

    public class Trips
    {
        private readonly HITWDbContext _dbContext;
        public Trips(HITWDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [FunctionName("TripAdd")]
        public async Task<IActionResult> TripAdd(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Trips")] HttpRequest req,
            ILogger log)
        {
            var u = StaticWebAppAuth.Parse(req);
            var externalId = u.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _dbContext.Users.Where(x => x.ExternalId == externalId).SingleOrDefault();
            if (user is null)
            {
                return new StatusCodeResult((int)HttpStatusCode.Unauthorized);
            }

            var body = JsonConvert.DeserializeObject<Trip>(await new StreamReader(req.Body).ReadToEndAsync());

            var resp = await "https://beta3.api.climatiq.io/travel/flights"
                .WithOAuthBearerToken("XNVBAM6P1E46RQGPBXNWDAC9B916")
                .PostJsonAsync(new
                {
                    from = body.Departure,
                    to = body.Arrival,
                    passengers = 1,
                    class = "economy"
                }).GetJson<ClimatiqResp>();

            var trip = new Trip
            {
                Label = body.Label,
                Departure = body.Departure,
                Arrival = body.Arrival,
                IsRoundTrip = body.IsRoundTrip,
                UserId = user.Id,
            };

            _dbContext.Trips.Add(trip);
            _dbContext.SaveChanges();

            return new OkObjectResult(_dbContext.Trips.Where(x => x.UserId == user.Id).Select(x => new
            {
                label = x.Label,
                id = x.Id,
                co2 = x.Co2Kg,
                departure = x.Departure,
                arrival = x.Arrival,
                percentage = 0,
                isRoundTrip = x.IsRoundTrip,
            }).SingleOrDefault());
        }

        [FunctionName("TripsList")]
        public async Task<IActionResult> TripsList(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Trips")] HttpRequest req,
            ILogger log)
        {
            var u = StaticWebAppAuth.Parse(req);
            var externalId = u.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _dbContext.Users.Where(x => x.ExternalId == externalId).SingleOrDefault();

            if (user is null)
            {
                return new StatusCodeResult((int)HttpStatusCode.Unauthorized);
            }

            return new OkObjectResult(_dbContext.Trips.Where(x => x.UserId == user.Id).Select(x => new
            {
                label = x.Label,
                id = x.Id,
                co2 = x.Co2Kg,
                departure = x.Departure,
                arrival = x.Arrival,
                percentage = 0,
                isRoundTrip = x.IsRoundTrip,
            }).ToList());
        }
    }
}
