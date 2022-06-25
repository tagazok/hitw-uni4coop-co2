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
using Newtonsoft.Json;
using System.IO;
using System;

namespace HITW.Function
{
    public class Seeder
    {
        private readonly HITWDbContext _dbContext;

        public Seeder(HITWDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Veggie(int t0, int tripId)
        {
            _dbContext.Histories.Add(new History
            {
                Code = "VEGGIE",
                CreditInKgOfCo2 = 4,
                Date = DateTime.UtcNow.AddDays(-t0),
                TripId = tripId,
            });

            _dbContext.SaveChanges();
        }

        public void Shower(int t0, int tripId)
        {
            _dbContext.Histories.Add(new History
            {
                Code = "SHOWER",
                CreditInKgOfCo2 = 1.1m,
                Date = DateTime.UtcNow.AddDays(-t0),
                TripId = tripId,
            });

            _dbContext.SaveChanges();
        }

        public void Transportation(int t0, int tripId)
        {
            var km = new Random().Next(10, 40);

            _dbContext.Histories.Add(new History
            {
                Code = "TRANSPORTATION",
                CreditInKgOfCo2 = 110m * km * 0.67m / 1000m,
                Date = DateTime.UtcNow.AddDays(-t0),
                TripId = tripId,
                Data = $"{{\"distance\": {km}}}"
            });

            _dbContext.SaveChanges();
        }

        public void Donation(int t0, int tripId)
        {
            var amount = new Random().Next(20, 50);

            _dbContext.Histories.Add(new History
            {
                Code = "DONATION",
                CreditInKgOfCo2 = amount,
                Date = DateTime.UtcNow.AddDays(-t0),
                TripId = tripId,
                Data = $"{{\"amount\": {amount}}}"
            });

            _dbContext.SaveChanges();
        }

        [FunctionName("Seeder")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "seed/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            var bodyType = new {
                percentage = 0
            };

            var body = JsonConvert.DeserializeAnonymousType(await new StreamReader(req.Body).ReadToEndAsync(), bodyType);

            var action = new [] { Donation, Shower, Transportation, Veggie };
            var t0 = 0;

            do
            {
                var f = action[new Random().Next(0, action.Length)];
                f.Invoke(t0, id);
                t0++;
            } while (_dbContext.Histories.Where(x => x.TripId == id).Sum(x => x.CreditInKgOfCo2) / _dbContext.Trips.Where(x => x.Id == id).SingleOrDefault().Co2Kg * 100 < body.percentage);

            _dbContext.SaveChanges();
            return new OkResult();
        }
    }
}
