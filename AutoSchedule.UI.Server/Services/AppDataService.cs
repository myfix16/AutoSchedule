using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoSchedule.Core.Models;
using AutoSchedule.Core.Helpers;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;

namespace AutoSchedule.UI.Server.Services
{
    public class AppDataService
    {
        public IEnumerable<IEnumerable<Session>> AvailableSessions { get; set; } = new List<List<Session>>();
        public IEnumerable<string> AvailableClasses { get; set; } = new List<string>();
        public string Version { get; set; } = "1.0.0";
        public Schedule CurrentSchedule { get; set; } = new();
        public ObservableCollection<string> FilteredClasses { get; set; } = new();
        public ObservableCollection<string> SelectedClasses { get; set; } = new();
        public ObservableCollection<Schedule> AvailableSchedules { get; set; } = new();

        public async Task InitializeAsync()
        {
            if (AvailableSessions == null || !AvailableSessions.Any())
            {
                await GetSessionsFromDB();
                AvailableClasses = AvailableSessions
                    .Select(l => l.First().Name[0..8]).Distinct().OrderBy(s => s);
                foreach (var item in AvailableClasses) FilteredClasses.Add(item);
            }
        }

        private async Task GetSessionsFromDB()
        {
            var cosmosClient = new CosmosClientBuilder
                (DataProvider.GetDBConnectionString("AzureCosmosDB-ConnectionString-ReadOnly", Environment.GetEnvironmentVariable("VaultUri")))
               .WithSerializerOptions(new CosmosSerializationOptions { Indented = true })
               .Build();
            var container = cosmosClient.GetDatabase("SessionsData").GetContainer("SessionsContainer");

            var sqlQueryText = "SELECT * FROM c";
            var queryIterator = container.GetItemQueryIterator<Session>(new QueryDefinition(sqlQueryText));

            // Fetch session data from data base.
            List<Session> sessions = new();
            {
                //Asynchronous query execution
                while (queryIterator.HasMoreResults)
                {
                    foreach (var item in await queryIterator.ReadNextAsync()) sessions.Add(item);
                }
            }

            // reorder the data into IEnumerable<IEnumerable<Session>>.
            AvailableSessions = sessions.GroupBy(s => s.Name[0..8]);
        }
    }
}
