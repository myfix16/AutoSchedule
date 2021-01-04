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
                    new SessionTime(DayOfWeek.Friday,new Time(10,30),new Time(11,20))),
                new SingleSession("Lecture","CSC1001","1011","staff",
                    new SessionTime(DayOfWeek.Monday,new Time(8,30),new Time(9,50))),
                new SingleSession("Lecture","ENG2001","3021","staff",
                    new SessionTime(DayOfWeek.Tuesday,new Time(15,30),new Time(16,50))),
            };

            SingleSessions = new ObservableCollection<SingleSession>(sessions);
        }
    }
}
