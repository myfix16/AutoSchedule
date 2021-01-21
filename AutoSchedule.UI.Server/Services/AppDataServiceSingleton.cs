using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoSchedule.Core.Helpers;
using AutoSchedule.Core.Models;

namespace AutoSchedule.UI.Server.Services
{
    public class AppDataServiceSingleton
    {
        public IEnumerable<IEnumerable<Session>> AvailableSessions { get; set; }
        public IEnumerable<string> AvailableClasses { get; set; }
        public IDataProvider DataProvider { get; set; }
        public string Version = "1.0.0";

        public AppDataServiceSingleton()
        {
            DataProvider = new AzureCosmosDBDataProvider();
        }

        public async Task InitializeAsync()
        {
            if (AvailableSessions == null || !AvailableSessions.Any())
            {
                await GetSessionsFromDB();
                AvailableClasses = AvailableSessions
                    .Select(l => l.First().GetClassifiedName()).Distinct().OrderBy(s => s);
            }
        }

        /*private async Task GetSessionsFromDB()
        {
            // Get session data from Azure Cosmos DB.
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
                    foreach (var item in await queryIterator.ReadNextAsync()) 
                        sessions.Add(item);
                }
            }

            // reorder the data into IEnumerable<IEnumerable<Session>>.
            AvailableSessions = sessions.GroupBy(s => s.Name[0..9]);
        }*/

        private async Task GetSessionsFromDB()
        {
            // reorder the data into IEnumerable<IEnumerable<Session>>.
            AvailableSessions = (await DataProvider.GetSessionsAsync()).GroupBy(s => s.GetClassifiedName());
        }
    }
}
