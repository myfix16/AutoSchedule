using System;

using AutoSchedule.Win.ViewModels;

using Windows.UI.Xaml.Controls;

namespace AutoSchedule.Win.Views
{
    // For more info about the TabView Control see
    // https://docs.microsoft.com/uwp/api/microsoft.ui.xaml.controls.tabview?view=winui-2.2
    // For other samples, get the XAML Controls Gallery app http://aka.ms/XamlControlsGallery
    public sealed partial class SchedulePage : Page
    {
        public ScheduleViewModel ViewModel { get; } = new ScheduleViewModel();

        public SchedulePage()
        {
            InitializeComponent();
        }
    }
}
