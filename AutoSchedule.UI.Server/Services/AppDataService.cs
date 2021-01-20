using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoSchedule.Core.Models;

namespace AutoSchedule.UI.Server.Services
{
    public class AppDataService
    {
        public Schedule CurrentSchedule { get; set; } = new();
        public ObservableCollection<string> FilteredClasses { get; set; } = new();
        public ObservableCollection<string> SelectedClasses { get; set; } = new();
        public ObservableCollection<Schedule> AvailableSchedules { get; set; } = new();
        public string searchBoxText = string.Empty;
        public bool initialized = false;

        public async Task InitializeAsync(AppDataServiceSingleton appDataServiceSingleton)
        {
            if (!initialized)
            {
                await appDataServiceSingleton.InitializeAsync();
                foreach (var item in appDataServiceSingleton.AvailableClasses) FilteredClasses.Add(item);
                initialized = true;
            }
        }
    }
}
