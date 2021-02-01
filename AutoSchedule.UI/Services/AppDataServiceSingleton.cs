using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoSchedule.Core.Helpers;
using AutoSchedule.Core.Models;

namespace AutoSchedule.UI.Services
{
    public class AppDataServiceSingleton
    {
        public IEnumerable<IEnumerable<Session>> AvailableSessions;
        public IEnumerable<string> AvailableClasses;
        public IDataProvider<IEnumerable<Session>> DataProvider;

        public string Version = "1.1.0";

        public AppDataServiceSingleton() => DataProvider = new WebAPIDataProvider();

        public async Task InitializeAsync()
        {
            if (AvailableSessions == null || !AvailableSessions.Any())
            {
                AvailableSessions= (await DataProvider.GetSessionsAsync()).GroupBy(s => s.GetClassifiedName());
                AvailableClasses = AvailableSessions
                    .Select(l => l.First().GetClassifiedName()).Distinct().OrderBy(s => s);
            }
        }
    }
}
