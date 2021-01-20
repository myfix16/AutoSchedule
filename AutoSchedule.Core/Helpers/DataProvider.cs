using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AutoSchedule.Core.Models;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using CsvHelper;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;

namespace AutoSchedule.Core.Helpers
{
    public class DataProvider
    {
        public static readonly JsonSerializerOptions defaultJsonSerializerOptions
            = new()
            {
                WriteIndented = true,
            };

        public static string GetDBConnectionString(string vaultKey, string vaultUri)
        {
            var client = new SecretClient(vaultUri: new Uri(vaultUri), credential: new DefaultAzureCredential());
            return client.GetSecret(vaultKey).Value.Value;
        }

        public static IEnumerable<IEnumerable<Session>> GetSessionsFromCsvs(string dirPath)
        {
            var directoryInfo = new DirectoryInfo(dirPath);
            var sessions = new List<List<Session>>();
            foreach (var csvFile in directoryInfo.GetFiles("*.csv"))
            {
                sessions.Add(ReadSessions(csvFile.FullName) as List<Session>);
            }
            return sessions;
        }

        public static async Task<IEnumerable<Session>> GetSessionsFromDB()
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
            {
                //Asynchronous query execution
                while (queryIterator.HasMoreResults)
                {
                    foreach (var item in await queryIterator.ReadNextAsync())
                        sessions.Add(item);
                }
            }

            return sessions;
        }

        public static IEnumerable<Session> ReadSessions(string csvPath)
        {
            var sessions = new List<Session>();
            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                var codeField = csv.GetField("Code");
                var name = $"{csv.GetField("Name").Split('-')[0]} {codeField[0..3]}";
                var sessionType = codeField[4..7];
                var instructor = csv.GetField("Instructor");
                var timesField = csv.GetField("Time").Split(';');
                var sessionTimes = new List<SessionTime>();
                foreach (var timeString in timesField)
                {
                    var splittedTime = timeString.Split(' ');
                    // Do something.
                    for (int i = 0; i < splittedTime[0].Length; i += 2)
                    {
                        var dayOfWeek = splittedTime[0][i..(i + 2)] switch
                        {
                            "Mo" => DayOfWeek.Monday,
                            "Tu" => DayOfWeek.Tuesday,
                            "We" => DayOfWeek.Wednesday,
                            "Th" => DayOfWeek.Thursday,
                            "Fr" => DayOfWeek.Friday,
                            _ => throw new NotImplementedException()
                        };
                        sessionTimes.Add(new SessionTime(dayOfWeek, new Time(splittedTime[1]), new Time(splittedTime[3])));
                    }
                }
                // 2. split each item such as WeFr 10:00-11:00
                sessions.Add(new Session(sessionType, name, codeField[8..12], instructor, sessionTimes));
            }
            return sessions;
        }

        #region temp useless
        public static async Task WriteSessionsToJsonAsync<T>(
            T availableSessions, string fileName, JsonSerializerOptions options = null)
        {
            using FileStream createStream = File.Create(fileName);
            options ??= defaultJsonSerializerOptions;
            await JsonSerializer.SerializeAsync(createStream, availableSessions, options);
        }

        public static async ValueTask<TResult> ReadJsonToSessionsAsync<TResult>(string jsonPath)
        {
            using FileStream openStream = File.OpenRead(jsonPath);
            return await JsonSerializer.DeserializeAsync<TResult>(openStream, defaultJsonSerializerOptions);
        }

        public static async ValueTask<TResult> RequestSessionFromServer<TResult>(string url)
        {
            using var client = new HttpClient();
            using var stream = await client.GetStreamAsync(url);
            return await JsonSerializer.DeserializeAsync<TResult>(stream);
        }

        public static async ValueTask<string> RequestJsonFromServer(string url)
        {
            using var client = new HttpClient();
            return await client.GetStringAsync(url);
        }
        #endregion
    }
}
