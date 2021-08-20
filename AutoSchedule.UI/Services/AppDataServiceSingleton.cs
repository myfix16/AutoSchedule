using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public readonly ObservableCollection<Session> AvailableSessionsFlat = new();

        /// <summary>
        /// All available sessions, grouped by class name.
        /// </summary>
        public IEnumerable<IEnumerable<Session>> AvailableSessions;

        /// <summary>
        /// All available class, container of sessions, such as ACT2111.
        /// </summary>
        public IEnumerable<string> AvailableClasses;

        private readonly IDataProvider<IEnumerable<Session>> DataProvider;

        public const string Version = "1.1.0";

        public const string Term = "2021-2022 Term 1";

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
            if (!initialized)
            {
                foreach (var item in await DataProvider.GetSessionsAsync())
                    AvailableSessionsFlat.Add(item);

                AvailableSessions = AvailableSessionsFlat.GroupBy(s => s.GetClassifiedName());
                AvailableClasses = AvailableSessions
                    .Select(l => l.First().GetClassifiedName()).Distinct().OrderBy(s => s);
                initialized = true;

#if DEBUG
                Console.WriteLine("DataService singleton initialized.");
#endif
            }
        }
    }
}
