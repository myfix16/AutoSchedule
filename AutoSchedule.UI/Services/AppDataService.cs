using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoSchedule.Core.Models;

namespace AutoSchedule.UI.Services
{
    public class AppDataService
    {
        public IEnumerable<IEnumerable<Session>> AvailableSessions { get; set; } = new List<List<Session>>();
        public IEnumerable<string> AvailableClasses { get; set; } = new List<string>();
        public string Version { get; set; } = "1.0.0 Preview";
        public Schedule CurrentSchedule { get; set; } = new();
        public ObservableCollection<string> FilteredClasses { get; set; } = new();
        public ObservableCollection<string> SelectedClasses { get; set; } = new();
        public ObservableCollection<Schedule> AvailableSchedules { get; set; } = new();

        public async Task InitializeAsync(HttpClient Http, string path)
        {
            if (AvailableSessions == null || !AvailableSessions.Any())
            {
                AvailableSessions = await Http.GetFromJsonAsync<List<List<Session>>>(path);
                AvailableClasses = AvailableSessions
                    .Select(l => l.First().Name).Distinct().OrderBy(s => s);
                foreach (var item in AvailableClasses) FilteredClasses.Add(item);
            }
        }
    }
}
