using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoSchedule.Core.Models;
using AutoSchedule.Win.Helpers;
using Windows.UI.Xaml.Controls;

namespace AutoSchedule.Win.ViewModels
{
    public class MainViewModel : Observable
    {
        public List<List<Session>> AvailableSessions { get; set; }
        public ObservableCollection<string> AvailableClasses { get; set; }
        public ObservableCollection<string> SelectedClasses { get; set; }

        //private ObservableCollection<string> _filteredClasses;
        // TODO: [Optional] Sort the class list.
        public ObservableCollection<string> FilteredClasses { get; set; }

        private string _searchBoxText;
        public string SearchBoxText
        {
            get => _searchBoxText;
            set
            {
                _searchBoxText = value;
                OnFilterChanged();
            }
        }

        // TODO: Add Multiple Selection Support. 
        public string SelectedAvailableClass { get; set; }
        public string SelectedSelectedClass { get; set; }

        private RelayCommand _addClassToListCommand;
        private RelayCommand _removeClassFromListCommand;
        private RelayCommand _findScheduleCommand;

        public RelayCommand AddClassToListCommand => _addClassToListCommand ??= new RelayCommand(AddClassToList);
        public RelayCommand RemoveClassFromListCommand => _removeClassFromListCommand ??= new RelayCommand(RemoveClassFromList);
        public RelayCommand FindScheduleCommand => _findScheduleCommand ??= new RelayCommand(FindSchedule);

        public List<Schedule> PossibleSchedules { get; set; }

        private void FindSchedule()
        {
            PossibleSchedules = ClassSelector.FindSchedules(
                AvailableSessions.Where(c => SelectedClasses.Contains(c.First().Name)));
        }

        private void RemoveClassFromList()
        {
            if (SelectedSelectedClass == null) return;

            AvailableClasses.Add(SelectedSelectedClass);
            OnFilterChanged();
            SelectedClasses.Remove(SelectedSelectedClass);
        }

        private void AddClassToList()
        {
            if (SelectedAvailableClass == null) return;

            SelectedClasses.Add(SelectedAvailableClass);
            AvailableClasses.Remove(SelectedAvailableClass);
            if (FilteredClasses.Contains(SelectedAvailableClass))
            {
                FilteredClasses.Remove(SelectedAvailableClass);
            }
        }

        public void OnFilterChanged()
        {
            var filtered = (SearchBoxText == null || SearchBoxText == "")
                ? AvailableClasses : AvailableClasses.Where(c => Filter(c));
            Remove_NonMatching(filtered);
            AddBack_Contacts(filtered);
        }

        private bool Filter(string class_)
            => class_.Contains(SearchBoxText, StringComparison.InvariantCultureIgnoreCase);

        private void Remove_NonMatching(IEnumerable<string> filteredData)
        {
            for (int i = FilteredClasses.Count - 1; i >= 0; i--)
            {
                var item = FilteredClasses[i];
                if (!filteredData.Contains(item))
                {
                    FilteredClasses.Remove(item);
                }
            }
        }

        private void AddBack_Contacts(IEnumerable<string> filteredData)
        {
            foreach (var item in filteredData)
            {
                if (!FilteredClasses.Contains(item))
                {
                    FilteredClasses.Add(item);
                }
            }
        }

        public MainViewModel()
        {
            AvailableSessions = new List<List<Session>>
            {
                // FIN 3080
                new List<Session>
                {
                    new Session
                        ("Lecture","FIN3080","1369","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Thursday,new Time(8,30),new Time(10,20)),
                            new SessionTime(DayOfWeek.Friday,new Time(8,30),new Time(9,20)),
                        }),
                    new Session
                        ("Lecture","FIN3080","1370","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Thursday,new Time(10,30),new Time(12,20)),
                            new SessionTime(DayOfWeek.Friday,new Time(10,30),new Time(11,20)),
                        }),
                    new Session
                        ("Lecture","FIN3080","1371","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Thursday,new Time(13,30),new Time(15,20)),
                            new SessionTime(DayOfWeek.Friday,new Time(13,30),new Time(14,20)),
                        }),
                },
                // FIN 4110
                new List<Session>
                {
                    new Session
                        ("Lecture","FIN4110","1384","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Wednesday,new Time(10,30),new Time(11,50)),
                            new SessionTime(DayOfWeek.Friday,new Time(9,00),new Time(10,20)),
                        }),
                    new Session
                        ("Lecture","FIN4110","1385","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Wednesday,new Time(13,30),new Time(14,50)),
                            new SessionTime(DayOfWeek.Friday,new Time(10,30),new Time(11,50)),
                        }),
                },
                // FIN 4210
                new List<Session>
                {
                    new Session
                        ("Lecture","FIN4210","1778","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Tuesday,new Time(10,30),new Time(11,50)),
                            new SessionTime(DayOfWeek.Thursday,new Time(10,30),new Time(11,50)),
                        }),
                },
                // MAT 3007
                new List<Session>
                {
                    new Session
                        ("Lecture","MAT3007","1445","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Wednesday,new Time(14,00),new Time(15,20)),
                            new SessionTime(DayOfWeek.Friday,new Time(14,00),new Time(15,20)),
                        }),
                },
                // MAT 2002
                new List<Session>
                {
                    new Session
                        ("Lecture","MAT2002","1426","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Tuesday,new Time(10,30),new Time(11,50)),
                            new SessionTime(DayOfWeek.Thursday,new Time(10,30),new Time(11,50)),
                        }),
                    new Session
                        ("Lecture","MAT2002","1427","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Tuesday,new Time(13,30),new Time(14,50)),
                            new SessionTime(DayOfWeek.Thursday,new Time(13,30),new Time(14,50)),
                        }),
                },
                // FTE 4560
                new List<Session>
                {
                    new Session
                        ("Lecture","FTE4560","2062","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Tuesday,new Time(8,30),new Time(9,50)),
                            new SessionTime(DayOfWeek.Thursday,new Time(8,30),new Time(9,50)),
                        }),
                },
                // DMS 3003
                new List<Session>
                {
                    new Session
                        ("Lecture","DMS3003","1936","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Tuesday,new Time(8,30),new Time(9,50)),
                            new SessionTime(DayOfWeek.Thursday,new Time(8,30),new Time(9,50)),
                        }),
                    new Session
                        ("Lecture","DMS3003","1937","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Tuesday,new Time(10,30),new Time(11,50)),
                            new SessionTime(DayOfWeek.Thursday,new Time(10,30),new Time(11,50)),
                        }),
                },
                // GEB 2404
                new List<Session>
                {
                    new Session
                        ("Lecture","GEB2404","2111","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Friday,new Time(8,30),new Time(10,20)),
                        }),
                },
                // GEB 2404 TUT
                new List<Session>
                {
                    new Session
                        ("Lecture","GEB2404T","2112","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Wednesday,new Time(8,30),new Time(10,20)),
                        }),
                    new Session
                        ("Lecture","GEB2404T","2113","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Wednesday,new Time(10,30),new Time(12,20)),
                        }),
                    new Session
                        ("Lecture","GEB2404T","2114","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Wednesday,new Time(15,30),new Time(17,20)),
                        }),
                },
                // DDA 4230
                new List<Session>
                {
                    new Session
                        ("Lecture","DDA4230","2059","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Monday,new Time(13,30),new Time(14,50)),
                            new SessionTime(DayOfWeek.Wednesday,new Time(13,30),new Time(14,50)),
                        }),
                },
                // CSC 3170
                new List<Session>
                {
                    new Session
                        ("Lecture","CSC3170","1616","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Monday,new Time(10,30),new Time(11,50)),
                            new SessionTime(DayOfWeek.Wednesday,new Time(10,30),new Time(11,50)),
                        }),
                    new Session
                        ("Lecture","CSC3170","1617","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Monday,new Time(13,30),new Time(14,50)),
                            new SessionTime(DayOfWeek.Wednesday,new Time(13,30),new Time(14,50)),
                        }),
                },
                // RMS 4060
                new List<Session>
                {
                    new Session
                        ("Lecture","RMS4060","1737","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Monday,new Time(15,30),new Time(16,50)),
                            new SessionTime(DayOfWeek.Wednesday,new Time(15,30),new Time(16,50)),
                        }),
                },
                // CSC4020
                new List<Session>
                {
                    new Session
                        ("Lecture","CSC4020","1642","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Monday,new Time(8,30),new Time(9,50)),
                            new SessionTime(DayOfWeek.Wednesday,new Time(8,30),new Time(9,50)),
                        }),
                },
                // ERG3020
                new List<Session>
                {
                    new Session
                        ("Lecture","ERG3020","1744","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Monday,new Time(10,30),new Time(11,50)),
                            new SessionTime(DayOfWeek.Wednesday,new Time(10,30),new Time(11,50)),
                        }),
                },
                // STA3010
                new List<Session>
                {
                    new Session
                        ("Lecture","STA3010","1690","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Monday,new Time(9,00),new Time(10,20)),
                            new SessionTime(DayOfWeek.Wednesday,new Time(9,00),new Time(10,20)),
                        }),
                    new Session
                        ("Lecture","STA3010","1691","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Monday,new Time(13,30),new Time(14,50)),
                            new SessionTime(DayOfWeek.Wednesday,new Time(13,30),new Time(14,50)),
                        }),
                },
                // CSC 3050
                new List<Session>
                {
                    new Session
                        ("Lecture","CSC3050","1607","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Tuesday,new Time(13,30),new Time(15,50)),
                            new SessionTime(DayOfWeek.Thursday,new Time(13,30),new Time(15,50)),
                        }),
                    new Session
                        ("Lecture","CSC3050","1608","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Tuesday,new Time(10,30),new Time(11,50)),
                            new SessionTime(DayOfWeek.Thursday,new Time(10,30),new Time(11,50)),
                        }),

                },
                // CSC 4180
                new List<Session>
                {
                    new Session
                        ("Lecture","CSC3050","1607","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Tuesday,new Time(10,30),new Time(11,50)),
                            new SessionTime(DayOfWeek.Thursday,new Time(10,30),new Time(11,50)),
                        }),

                },
                // EIE 2810
                new List<Session>
                {
                    new Session
                        ("Lecture","EIE2810","1781","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Thursday,new Time(15,00),new Time(17,50)),
                        }),
                    new Session
                        ("Lecture","EIE2810","1782","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Friday,new Time(9,00),new Time(11,50)),
                        }),

                },
                // GEB 2405
                new List<Session>
                {
                    new Session
                        ("Lecture","GEB2405","1969","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Friday,new Time(8,30),new Time(9,20)),
                        }),
                    new Session
                        ("Lecture","GEB2405","1970","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Friday,new Time(10,30),new Time(11,20)),
                        }),
                    new Session
                        ("Lecture","GEB2405","1971","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Friday,new Time(14,30),new Time(14,50)),
                        }),
                },
                // ENG2002S
                new List<Session>
                {
                    new Session
                        ("Lecture","ENG2002S","1821","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Monday,new Time(9,00),new Time(10,20)),
                            new SessionTime(DayOfWeek.Wednesday,new Time(9,00),new Time(10,20)),
                        }),
                    new Session
                        ("Lecture","ENG2002S","1691","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Monday,new Time(13,30),new Time(14,50)),
                            new SessionTime(DayOfWeek.Wednesday,new Time(13,30),new Time(14,50)),
                        }),
                },
                // ENG2002B
                new List<Session>
                    {
                        new Session
                            ("Lecture","ENG2002B","????","staff",
                            new List<SessionTime>
                            {
                                new SessionTime(DayOfWeek.Tuesday,new Time(15,30),new Time(16,50)),
                                new SessionTime(DayOfWeek.Thursday,new Time(15,30),new Time(16,50)),
                            }),
                    },
            };
            AvailableClasses = new ObservableCollection<string>(AvailableSessions
                .Select(l => l.First().Name).Distinct());
            FilteredClasses = new ObservableCollection<string>(AvailableClasses);
            SelectedClasses = new ObservableCollection<string>();
        }
    }
}
