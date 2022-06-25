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
            ILogger log, int id)
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
                SHOWER = _dbContext
                    .Histories
                    .Where(x => x.UserId == id)
                    .Where(x => x.Code == "SHOWER")
                    .Count(),
                VEGGIE = _dbContext
                    .Histories
                    .Where(x => x.UserId == id)
                    .Where(x => x.Code == "VEGGIE")
                    .Count(),
                THERMOSTAT = _dbContext
                    .Histories
                    .Where(x => x.UserId == id)
                    .Where(x => x.Code == "THERMOSTAT")
                    .Count(),
                RECYCLING = _dbContext
                    .Histories
                    .Where(x => x.UserId == id)
                    .Where(x => x.Code == "RECYCLING")
                    .Count(),
                PLASTIC = _dbContext
                    .Histories
                    .Where(x => x.UserId == id)
                    .Where(x => x.Code == "PLASTIC")
                    .Count(),
                COMPUTER = _dbContext
                    .Histories
                    .Where(x => x.UserId == id)
                    .Where(x => x.Code == "COMPUTER")
                    .Count(),
                totalDistance = _dbContext
                    .Histories
                    .Where(x => x.UserId == id)
                    .Where(x => x.Code == "TRANSPORTATION")
                    .ToList()
                    .Select(x => JsonConvert.DeserializeAnonymousType(x.Data, modelDistance))
                    .Sum(x => x.distance),
                totalDonation = _dbContext
                    .Histories
                    .Where(x => x.UserId == id)
                    .Where(x => x.Code == "DONATION")
                    .ToList()
                    .Select(x => JsonConvert.DeserializeAnonymousType(x.Data, modelDonation))
                    .Sum(x => x.amount),

                totalTrip = _dbContext.Trips.Where(x => x.UserId == id).Count(),
                totalFunded = _dbContext.Trips
                    .Where(x => x.UserId == id)
                    .Select(x => (x.Histories.Sum(y => y.CreditInKgOfCo2) / x.Co2Kg) >= 1)
                    .Count(x => x == true),
            };

            return new OkObjectResult(profile);
        }
    }
}
