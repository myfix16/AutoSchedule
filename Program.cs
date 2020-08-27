using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoSchedule
{
    class Program
    {
        static void Main(string[] args)
        {
            #region test data

            List<List<Session>> allLectures = new List<List<Session>>
            {
                new List<Session>
                {
                    new Session
                        (SessionType.Lecture,"FIN2010 L01","1841","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Tuesday,new Time(8,30),new Time(9,50)),
                            new SessionTime(DayOfWeek.Thursday,new Time(8,30),new Time(9,50)),
                        }),
                    new Session
                        (SessionType.Lecture,"FIN2010 L02","1841Copy","mx",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Tuesday,new Time(8,30),new Time(9,50)),
                            new SessionTime(DayOfWeek.Thursday,new Time(8,30),new Time(9,50)),
                        }),

                },
                new List<Session>
                {
                    new Session
                        (SessionType.Lecture,"ECO3121 L01","1515","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Monday,new Time(8,30),new Time(9,50)),
                            new SessionTime(DayOfWeek.Wednesday,new Time(8,30),new Time(9,50)),
                        }),

                },
                new List<Session>
                {
                    new Session
                        (SessionType.Lecture,"MAT2040 L01","1437","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Tuesday,new Time(13,30),new Time(14,50)),
                            new SessionTime(DayOfWeek.Thursday,new Time(13,30),new Time(14,50)),
                        }),

                },
                new List<Session>
                {
                    new Session
                        (SessionType.Lecture,"GFH1000 L01","1210","staff",
                        new List<SessionTime>
                        {
                            new SessionTime(DayOfWeek.Friday,new Time(13,00),new Time(13,50)),
                        })
                },
            };

            var a = new Dictionary<string, Session>();

            #endregion


            // TODO: Retrieve class data from OCR outcome.

            // Generate all possible solutions.
            Console.WriteLine("Lectures:");
            ClassSelector.FindSchedules(allLectures)
                         .ForEach(s => 
                                  { 
                                      s.Lectures.ForEach(Console.WriteLine);
                                      Console.WriteLine();
                                  });

            Console.WriteLine();

            Console.WriteLine("Hello World!");

            // TODO: Something else.
        }

    }
}
