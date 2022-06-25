using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using HITW.Function.Helpers;
using api.Data;
using api.Data.Models;
using System.Linq;
using System.Security.Claims;
using System;
using Newtonsoft.Json;

namespace HITW.Function
{
    public class RewardReq
    {
        public string Code { get; set; }
        public int Distance { get; set; }
        public int TripId { get; set; }
    }


    public class Reward
    {
        private readonly HITWDbContext _dbContext;

        public Reward(HITWDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [FunctionName("Reward")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "rewards")] HttpRequest req,
            ILogger log)
        {
            var u = StaticWebAppAuth.Parse(req);
            var id = u.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _dbContext.Users.Where(x => x.ExternalId == id).SingleOrDefault();

            var rreq = JsonConvert.DeserializeObject<RewardReq>(await req.ReadAsStringAsync());

            var alreadyWonToday = from r in _dbContext.Histories
                                  where r.UserId == user.Id
                                  where r.Code == rreq.Code
                                  where r.Date == DateTime.Today
                                  select r;

            if (alreadyWonToday.Any())
            {
                return new BadRequestObjectResult("You already won today");
            }

            // todo: map code to credit in co2
            var creditInKgOfCo2 = rreq.Code switch
            {
                "VEGGIE" => Calculation.GoVeggie(),
                "TRANSPORTATION" => Calculation.TakePublicTransportationOrBicycle(rreq.Distance),
                "SHOWER" => Calculation.TakeShowerInsteadOfBath(),
                "PLASTIC" => Calculation.ReusePlasticBag(),
                "COMPUTER" => Calculation.TurnOffComputers(),
                "THERMOSTAT" => Calculation.TurnDownThermostats(),
                "RECYCLING" => Calculation.Recycling(),
                _ => throw new ArgumentOutOfRangeException($"Invalid public code: {rreq.Code}")
            };

            if (!alreadyWonToday.Any())
            {
                _dbContext.Histories.Add(new History
                {
                    Code = rreq.Code,
                    CreditInKgOfCo2 = creditInKgOfCo2,
                    Date = DateTime.UtcNow,
                    UserId = user.Id,
                    TripId = rreq.TripId
                });

                _dbContext.SaveChanges();
            }

            return new OkResult();
        }
    }
}
