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

namespace HITW.Function
{
    public class Register
    {
        private readonly HITWDbContext _dbContext;

        public Register(HITWDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [FunctionName("Register")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var u = StaticWebAppAuth.Parse(req);

            var id = u.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var name = u.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            if (_dbContext.Users.Where(x => x.ExternalId == id).FirstOrDefault() is null)
            {
                _dbContext.Users.Add(new User
                {
                    Firtsname = name,
                    ExternalId = id,
                });
            }
            else
            {
                var maybeUser = _dbContext.Users.Where(x => x.ExternalId == id).FirstOrDefault();
                maybeUser.Firtsname = name;
            }

            _dbContext.SaveChanges();

            var currentUser = _dbContext.Users.Where(x => x.ExternalId == id).FirstOrDefault();

            return new OkObjectResult(currentUser);
        }
    }
}
