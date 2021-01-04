using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using AutoSchedule.Core.Models;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

namespace AutoSchedule.Win.Controls
{
    public sealed class AcademicCalendar : Control
    {
        private Grid _mainGrid;

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(AcademicCalendar),
                new PropertyMetadata(null, new PropertyChangedCallback(OnItemsSourcePropertyChanged)));

        public IEnumerable ItemsSource
        {
            get { return GetValue(ItemsSourceProperty) as IEnumerable; }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private static void OnItemsSourcePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is AcademicCalendar control)
                control.OnItemsSourceChanged((IEnumerable)e.OldValue, (IEnumerable)e.NewValue);
        }

        private void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            // Remove handler for oldValue.CollectionChanged
            if (oldValue is INotifyCollectionChanged oldValueINotifyCollectionChanged)
            {
                oldValueINotifyCollectionChanged.CollectionChanged -= new NotifyCollectionChangedEventHandler(NewValueINotifyCollectionChanged_CollectionChanged);
            }

            // Add handler for newValue.CollectionChanged (if possible)
            if (newValue is INotifyCollectionChanged newValueINotifyCollectionChanged)
            {
                newValueINotifyCollectionChanged.CollectionChanged += new NotifyCollectionChangedEventHandler(NewValueINotifyCollectionChanged_CollectionChanged);
            }

            // Invoke OnCollectionChanged
            NewValueINotifyCollectionChanged_CollectionChanged(null, null);
        }

        private async void NewValueINotifyCollectionChanged_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // ! To make sure the grid component is initialized first. May not be a good practice.
            if (_mainGrid == null) await Task.Delay(500);

            foreach (var item in ItemsSource as IEnumerable<SingleSession>)
            {
                var itemView = new AcademicCalendarItem
                {
                    ClassID = $"{item.Name} {item.Code}",
                    ClassType = item.SessionType,
                    ClassTime = item.SessionTime,
                };

                Grid.SetColumn(itemView, (int)itemView.ClassTime.DayOfWeek);
                Grid.SetRow(itemView, itemView.ClassTime.StartTime.Hour - 7);
                int classDuration = itemView.ClassTime.EndTime.TotalMinutes - itemView.ClassTime.StartTime.TotalMinutes;
                int rowSpan = classDuration % 60 == 0 ? classDuration / 60 : classDuration / 60 + 1;
                Grid.SetRowSpan(itemView, rowSpan);

                _mainGrid.Children.Add(itemView);
            }
        }

/*        protected override void OnApplyTemplate()
        {
            _mainGrid = GetTemplateChild("MainGrid") as Grid;
            base.OnApplyTemplate();
        }*/

        private void AcademicCalendar_Loaded(object sender, RoutedEventArgs e)
        {
            _mainGrid = GetTemplateChild("MainGrid") as Grid;

            // Draw grid borders
            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < 14; i++)
                {
                    Border border = new Border
                    {
                        BorderThickness = new Thickness(1),
                        BorderBrush = new SolidColorBrush(Colors.Gray),
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch
                    };

                    Grid.SetColumn(border, j);
                    Grid.SetRow(border, i);

                    _mainGrid.Children.Add(border);
                }
            }

            // Add time
            for (int i = 1; i < 14; i++)
            {
                TextBlock textBlock = new TextBlock
                {
                    Text = $"{7 + i}:30",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                Grid.SetColumn(textBlock, 0);
                Grid.SetRow(textBlock, i);

                _mainGrid.Children.Add(textBlock);
            }
        }

        public AcademicCalendar()
        {
            DefaultStyleKey = typeof(AcademicCalendar);
            Loaded += AcademicCalendar_Loaded;
        }
    }
}
