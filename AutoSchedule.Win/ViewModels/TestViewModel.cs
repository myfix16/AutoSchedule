using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoSchedule.Core.Models;
using AutoSchedule.Win.Helpers;

namespace AutoSchedule.Win.ViewModels
{
    public class TestViewModel : Observable
    {
        public ObservableCollection<SingleSession> SingleSessions { get; set; }

        public TestViewModel()
        {
            var sessions = new List<SingleSession>
            {
                new SingleSession("Lecture","GEB2404","2111","staff",
                    new SessionTime(DayOfWeek.Friday,new Time(10,30),new Time(11,50))),
            };

            SingleSessions = new ObservableCollection<SingleSession>(sessions);
        }
    }
}
