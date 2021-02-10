using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoSchedule.Core.Helpers;
using AutoSchedule.Core.Models;

namespace AutoSchedule.UI.Services
{
    public class AppDataServiceSingleton
    {
        /// <summary>
        /// All available sessions such as ACT2111 L01.
        /// </summary>
        public IEnumerable<Session> AvailableSessionsFlat;

        /// <summary>
        /// All available sessions, grouped by class name.
        /// </summary>
        public IEnumerable<IEnumerable<Session>> AvailableSessions;

        /// <summary>
        /// All available class, container of sessions, such as ACT2111.
        /// </summary>
        public IEnumerable<string> AvailableClasses;

        private readonly IDataProvider<IEnumerable<Session>> DataProvider;

        public string Version = "1.1.0";

        private bool initialized = false;

        public AppDataServiceSingleton(IDataProvider<IEnumerable<Session>> dataProvider)
        {
            DataProvider = dataProvider;
        }

        /// <summary>
        /// Retrieve session data from source.
        /// </summary>
        public async Task InitializeAsync()
        {
            //if (AvailableSessions == null || !AvailableSessions.Any())
            if (!initialized)
            {
                // TODO: Add notification of fetching data.
                AvailableSessionsFlat = await DataProvider.GetSessionsAsync();
                AvailableSessions = AvailableSessionsFlat.GroupBy(s => s.GetClassifiedName());
                AvailableClasses = AvailableSessions
                    .Select(l => l.First().GetClassifiedName()).Distinct().OrderBy(s => s);
                initialized = true;
            }
        }
    }
}
