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

namespace HITW.Function
{
    public class Trips
    {
        private HITWDbContext _dbContext;

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

            var trip = new Trip
            {
                Label = body.Label,
                Departure = body.Departure,
                Arrival = body.Arrival,
                IsRoundTrip = body.IsRoundTrip,
                UserId = user.Id,
            };

            return new OkObjectResult(_dbContext.Trips.Where(x => x.UserId == user.Id).Select(x => new
            {
                label = x.Label,
                id = x.Id,
                co2 = x.Co2Kg,
                departure = x.Departure,
                arrival = x.Arrival,
                percentage = 0,
                isRoundTrip = x.IsRoundTrip,
            }));
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
            }));
        }
    }
}