using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace AutoSchedule.Core.Helpers
{
    public static class DataProvider
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
    }
}
