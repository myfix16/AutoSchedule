using System;

using AutoSchedule.Win.ViewModels;

using Windows.UI.Xaml.Controls;

namespace AutoSchedule.Win.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
