using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using AutoSchedule.Core.Models;

namespace AutoSchedule.Core.Helpers
{
    public class WebAPIDataProvider : IDataProvider<IEnumerable<Session>>
    {
        [Obsolete("Use the async method instead.")]
        public IEnumerable<Session> GetSessions()
        {
            using var client = new HttpClient();
            return client.GetFromJsonAsync<IEnumerable<Session>>("https://api-autoschedule.azurewebsites.net/api/sessions").Result;
        }

        public async Task<IEnumerable<Session>> GetSessionsAsync()
        {
            using var client = new HttpClient();
            return await client.GetFromJsonAsync<IEnumerable<Session>>("https://api-autoschedule.azurewebsites.net/api/sessions");
        }
    }
}
