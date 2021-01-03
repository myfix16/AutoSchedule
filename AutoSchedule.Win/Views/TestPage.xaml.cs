using System;

using AutoSchedule.Win.ViewModels;

using Windows.UI.Xaml.Controls;

namespace AutoSchedule.Win.Views
{
    public sealed partial class TestPage : Page
    {
        public TestViewModel ViewModel { get; } = new TestViewModel();

        public TestPage()
        {
            InitializeComponent();
        }
    }
}
