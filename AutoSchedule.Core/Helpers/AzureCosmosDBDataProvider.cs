using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoSchedule.Core.Models;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;

namespace AutoSchedule.Core.Helpers
{
    /// <summary>
    /// A helper class that renders data from data source. 
    /// </summary>
    public class AzureCosmosDBDataProvider : IDataProvider<IEnumerable<Session>>
    {
        [Obsolete("Do not use sync method in this class.")]
        public IEnumerable<Session> GetSessions()
        {
            return GetSessionsAsync().Result;
        }

        public async Task<IEnumerable<Session>> GetSessionsAsync()
        {
            // Get session data from Azure Cosmos DB.
            var cosmosClient = new CosmosClientBuilder
                (GetDBConnectionString("AzureCosmosDB-ConnectionString-ReadOnly", Environment.GetEnvironmentVariable("VaultUri")))
               .WithSerializerOptions(new CosmosSerializationOptions { Indented = true })
               .Build();
            var container = cosmosClient.GetDatabase("SessionsData").GetContainer("SessionsContainer");

            var sqlQueryText = "SELECT * FROM c";
            var queryIterator = container.GetItemQueryIterator<Session>(new QueryDefinition(sqlQueryText));

            // Fetch session data from data base.
            List<Session> sessions = new();
            //Asynchronous query execution
            while (queryIterator.HasMoreResults)
            {
                foreach (var item in await queryIterator.ReadNextAsync())
                    sessions.Add(item);
            }

            return sessions;
        }

        private static string GetDBConnectionString(string vaultKey, string vaultUri)
        {
            var client = new SecretClient(vaultUri: new Uri(vaultUri), credential: new DefaultAzureCredential());
            return client.GetSecret(vaultKey).Value.Value;
        }
    }
}
