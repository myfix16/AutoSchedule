using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using AutoSchedule.Core.Helpers;
using AutoSchedule.Core.Models;
using AutoSchedule.UI.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor;

namespace AutoSchedule.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mzc3ODg3QDMxMzgyZTM0MmUzME5QQ1RISzNJYVorVm1oWjhSQjhvcjU0OFBHMm1QQlI2cVpKa1FqTzRXeGM9;Mzc3ODg4QDMxMzgyZTM0MmUzME9TOTRoUk9xN05JbHVPc1BndWtvWlNoVUUzbnNFMzg0K1pzK3NVT09YSE09");

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddSingleton<AppDataServiceSingleton>();
            builder.Services.AddScoped<AppDataService>();

            await builder.Build().RunAsync();
        }
    }
}
