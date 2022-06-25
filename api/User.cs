using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using api.Data;
using System.Linq;
using Newtonsoft.Json;

namespace HITW.Function
{
    public class UserFunction
    {
        private readonly HITWDbContext _dbContext;

        public UserFunction(HITWDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [FunctionName("Users")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/{id}")] HttpRequest req,
            ILogger log, string id)
        {

            var modelDistance = new
            {
                distance = 0
            };

            var modelDonation = new
            {
                amount = 0
            };

            var profile = new
            {
                SHOWER = (
                            from h in _dbContext.Histories
                            join t in _dbContext.Trips on h.TripId equals t.Id
                            join u in _dbContext.Users on t.UserId equals u.Id
                            where u.ExternalId == id
                            where h.Code == "SHOWER"
                            select h
                ).Count(),
                VEGGIE = (
                            from h in _dbContext.Histories
                            join t in _dbContext.Trips on h.TripId equals t.Id
                            join u in _dbContext.Users on t.UserId equals u.Id
                            where u.ExternalId == id
                            where h.Code == "VEGGIE"
                            select h
                ).Count(),
                THERMOSTAT = (
                            from h in _dbContext.Histories
                            join t in _dbContext.Trips on h.TripId equals t.Id
                            join u in _dbContext.Users on t.UserId equals u.Id
                            where u.ExternalId == id
                            where h.Code == "THERMOSTAT"
                            select h
                ).Count(),
                RECYCLING = (
                            from h in _dbContext.Histories
                            join t in _dbContext.Trips on h.TripId equals t.Id
                            join u in _dbContext.Users on t.UserId equals u.Id
                            where u.ExternalId == id
                            where h.Code == "RECYCLING"
                            select h
                ).Count(),
                PLASTIC = (
                            from h in _dbContext.Histories
                            join t in _dbContext.Trips on h.TripId equals t.Id
                            join u in _dbContext.Users on t.UserId equals u.Id
                            where u.ExternalId == id
                            where h.Code == "PLASTIC"
                            select h
                ).Count(),
                COMPUTER = (
                            from h in _dbContext.Histories
                            join t in _dbContext.Trips on h.TripId equals t.Id
                            join u in _dbContext.Users on t.UserId equals u.Id
                            where u.ExternalId == id
                            where h.Code == "COMPUTER"
                            select h
                ).Count(),

                totalDistance = (from h in _dbContext.Histories
                            join t in _dbContext.Trips on h.TripId equals t.Id
                            join u in _dbContext.Users on t.UserId equals u.Id
                            where u.ExternalId == id
                            where h.Code == "TRANSPORTATION" select h).ToList()
                    .Select(x => JsonConvert.DeserializeAnonymousType(x.Data, modelDistance))
                    .Sum(x => x.distance),

                totalDonation = (from h in _dbContext.Histories
                    join t in _dbContext.Trips on h.TripId equals t.Id
                    join u in _dbContext.Users on t.UserId equals u.Id
                    where u.ExternalId == id
                    where h.Code == "DONATION" select h).ToList()
                    .Select(x => JsonConvert.DeserializeAnonymousType(x.Data, modelDonation))
                    .Sum(x => x.amount),

                totalTrips =(from t in _dbContext.Trips join u in _dbContext.Users on t.UserId equals u.Id where u.ExternalId == id select t.Id).Count(),

                totalFunded = (from t in _dbContext.Trips join u in _dbContext.Users on t.UserId equals u.Id where u.ExternalId == id select (t.Histories.Sum(x => x.CreditInKgOfCo2) / t.Co2Kg) >= 1).Count(x => x == true),
                name = _dbContext.Users.Where(x => x.ExternalId == id).FirstOrDefault()?.Firtsname,
            };

            return new OkObjectResult(profile);
        }
    }
}
