using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoSchedule.Core.Models;

namespace AutoSchedule.UI.Services
{
    public class AppDataService
    {
        public Schedule CurrentSchedule = new();
        public ObservableCollection<string> FilteredClasses = new();
        public ObservableCollection<string> SelectedClasses = new();
        public ObservableCollection<Schedule> AvailableSchedules = new();

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
