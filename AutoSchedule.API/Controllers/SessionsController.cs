using System.Collections.Generic;
using System.Threading.Tasks;
using AutoSchedule.Core.Helpers;
using AutoSchedule.Core.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AutoSchedule.API.Controllers
{
    [EnableCors(Startup.CorsAllowSpecificOrigins)]
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private IEnumerable<Session> Sessions;

        private readonly IDataProvider<IEnumerable<Session>> DataProvider;

        public SessionsController()
        {
            DataProvider = new AzureCosmosDBDataProvider();
        }

        // GET: api/<SessionsController>
        [DisableCors]
        [HttpGet]
        public async Task<IEnumerable<Session>> GetSessions()
        {
            Sessions ??= await DataProvider.GetSessionsAsync();
            return Sessions;
        }
    }
}
