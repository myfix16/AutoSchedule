using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using AutoSchedule.Core.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace AutoSchedule.Win.Controls
{
    public sealed class AcademicCalendarItem : Control
    {
        // TODO: Adapt theme change.

        public readonly static DependencyProperty ClassIDProperty =
            DependencyProperty.Register("ClassID", typeof(string), typeof(AcademicCalendarItem), new PropertyMetadata(string.Empty));
        public readonly static DependencyProperty ClassTypeProperty =
            DependencyProperty.Register("ClassType", typeof(string), typeof(AcademicCalendarItem), new PropertyMetadata(string.Empty));
        public readonly static DependencyProperty ClassTimeProperty =
            DependencyProperty.Register("ClassTime", typeof(SessionTime), typeof(AcademicCalendarItem), new PropertyMetadata(new SessionTime()));

        public string ClassID
        {
            get { return (string)GetValue(ClassIDProperty); }
            set { SetValue(ClassIDProperty, value); }
        }
        public string ClassType
        {
            get { return (string)GetValue(ClassTypeProperty); }
            set { SetValue(ClassTypeProperty, value); }
        }
        public SessionTime ClassTime
        {
            get { return (SessionTime)GetValue(ClassTimeProperty); }
            set { SetValue(ClassTimeProperty, value); }
        }

        public AcademicCalendarItem()
        {
            DefaultStyleKey = typeof(AcademicCalendarItem);
        }
    }
}
