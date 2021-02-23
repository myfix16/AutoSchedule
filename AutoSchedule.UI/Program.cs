using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoSchedule.Core.Helpers;
using AutoSchedule.Core.Models;
using AutoSchedule.UI.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using BlazorFluentUI;

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
            builder.Services.AddBlazorFluentUI();
            builder.Services.AddSingleton<IDataProvider<IEnumerable<Session>>, WebAPIDataProvider>();
            builder.Services.AddSingleton<AppDataServiceSingleton>();
            builder.Services.AddScoped<AppDataService>();

            await builder.Build().RunAsync();
        }
    }
}
