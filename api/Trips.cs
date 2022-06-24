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
using Flurl;
using System;
using System.Net.Http;

namespace HITW.Function
{
    public class ClimatiqResp
    {
        [JsonProperty("co2e")]
        public double Co2e { get; set; }
    }
    public class ClimatiqReq
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("passengers")]
        public int Passengers { get; set; }

        [JsonProperty("class")]
        public string Cl { get; set; }
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
            var apiKey = Environment.GetEnvironmentVariable("ClimatiqApiKey");

            var u = StaticWebAppAuth.Parse(req);
            var externalId = u.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _dbContext.Users.Where(x => x.ExternalId == externalId).SingleOrDefault();
            if (user is null)
            {
                return new StatusCodeResult((int)HttpStatusCode.Unauthorized);
            }

            var body = JsonConvert.DeserializeObject<Trip>(await new StreamReader(req.Body).ReadToEndAsync());

            var payload =   new {
                        legs = new []
                        {
                            new ClimatiqReq
                            {
                                From =  body.Departure,
                                To = body.Arrival,
                                Passengers = 1,
                                Cl = "economy"
                            }
                        }};

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");

            var rawResp = await httpClient.PostAsJsonAsync("https://beta3.api.climatiq.io/travel/flights", payload);

            var resp = JsonConvert.DeserializeObject<ClimatiqResp>(await rawResp.Content.ReadAsStringAsync());

            var trip = new Trip
            {
                Label = body.Label,
                Departure = body.Departure,
                Arrival = body.Arrival,
                IsRoundTrip = body.IsRoundTrip,
                UserId = user.Id,
                Co2Kg = (int)Math.Round(resp.Co2e),
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
                percentage = new Random().Next(0, 100),
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

            return new OkObjectResult(_dbContext.Trips.Where(x => x.UserId == user.Id)
                .OrderByDescending(x => x.Id)
                .Select(x => new
            {
                label = x.Label,
                id = x.Id,
                co2 = x.Co2Kg,
                departure = x.Departure,
                arrival = x.Arrival,
                percentage = new Random().Next(0, 100),
                isRoundTrip = x.IsRoundTrip,
            }).ToList());
        }
    }
}
