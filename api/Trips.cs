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
using Microsoft.EntityFrameworkCore;

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

            var payload = new
            {
                legs = new[]
                        {
                            new ClimatiqReq
                            {
                                From =  body.Departure,
                                To = body.Arrival,
                                Passengers = 1,
                                Cl = "economy"
                            }
                        }
            };

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");

            var rawResp = await httpClient.PostAsJsonAsync("https://beta3.api.climatiq.io/travel/flights", payload);

            var resp = JsonConvert.DeserializeObject<ClimatiqResp>(await rawResp.Content.ReadAsStringAsync());

            if (body.IsRoundTrip == true)
            {
                resp.Co2e *= 2;
            }

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

            return new OkObjectResult(_dbContext.Trips
                .Where(x => x.UserId == user.Id)
                .Where(x => x.Id == trip.Id)
                .Select(x => new
            {
                label = x.Label,
                id = x.Id,
                co2 = x.Co2Kg,
                departure = x.Departure,
                arrival = x.Arrival,
                percentage = Math.Round((decimal)x.Histories.Sum(y => y.CreditInKgOfCo2) / (x.Co2Kg ?? 1) * 100),
                isRoundTrip = x.IsRoundTrip,
                rewards = Array.Empty<string>(),
            }).SingleOrDefault());
        }

        [FunctionName("TripGet")]
        public async Task<IActionResult> TripGet(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Trips/{id}")] HttpRequest req,
            int id,
            ILogger log)
        {
            var u = StaticWebAppAuth.Parse(req);
            var externalId = u.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _dbContext.Users.Where(x => x.ExternalId == externalId).SingleOrDefault();
            if (user is null)
            {
                return new StatusCodeResult((int)HttpStatusCode.Unauthorized);
            }
            var trip = _dbContext.Trips.Include(x => x.Histories).Where(x => x.Id == id && x.UserId == user.Id)
                .Include(x => x.Histories)
                .SingleOrDefault();
            if (trip is null)
            {
                return new StatusCodeResult((int)HttpStatusCode.NotFound);
            }
            return new OkObjectResult(new
            {
                label = trip.Label,
                id = trip.Id,
                co2 = trip.Co2Kg,
                departure = trip.Departure,
                arrival = trip.Arrival,
                percentage = trip.Histories.Sum(x => x.CreditInKgOfCo2) / (trip.Co2Kg ?? 1) * 100,
                isRoundTrip = trip.IsRoundTrip,
                rewards = trip.Histories.OrderByDescending(x => x.Date).Select(x => new
                {
                    code = x.Code,
                    creditInKgOfCo2 = x.CreditInKgOfCo2,
                    date = x.Date,
                    tripId = x.TripId,
                    rewardId = x.Id,
                }).ToList(),
            });
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

            return new OkObjectResult(new
            {
                totalCo2 = _dbContext.Trips.Where(x => x.UserId == user.Id).Sum(x => x.Co2Kg),
                trips = _dbContext.Trips.Where(x => x.UserId == user.Id)
                        .OrderByDescending(x => x.Id)
                        .Select(x => new
                        {
                            label = x.Label,
                            id = x.Id,
                            co2 = x.Co2Kg,
                            departure = x.Departure,
                            arrival = x.Arrival,
                            percentage = x.Histories.Sum(y => y.CreditInKgOfCo2) / (x.Co2Kg ?? 1) * 100,
                            isRoundTrip = x.IsRoundTrip,
                        }).ToList()
            });
        }

        [FunctionName("TripDelete")]
        public async Task<IActionResult> TripDelete(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Trips/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            var u = StaticWebAppAuth.Parse(req);
            var externalId = u.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _dbContext.Users.Where(x => x.ExternalId == externalId).SingleOrDefault();
            if (user is null)
            {
                return new StatusCodeResult((int)HttpStatusCode.Unauthorized);
            }

            var trip = _dbContext.Trips.Where(x => x.Id == id && x.UserId == user.Id).SingleOrDefault();
            if (trip is null)
            {
                return new StatusCodeResult((int)HttpStatusCode.NotFound);
            }

            _dbContext.Trips.Remove(trip);
            await _dbContext.SaveChangesAsync();
            return new OkResult();
        }
    }
}
