using System;
using System.Linq;
using System.Collections.Generic;

namespace AutoSchedule
{
    class Program
    {
        static void Main(string[] args)
        {
            #region test data

            List<Class> allClasses = new List<Class>
            {
                new Class("FIN2020", "someone")
                {
                    lectures = new List<LectureSession>
                    {
                        new LectureSession
                            ("L01","1841","staff",
                            new List<SessionTime>
                            {
                                new SessionTime(DayOfWeek.Tuesday,new Time(8,30),new Time(9,50)),
                                new SessionTime(DayOfWeek.Thursday,new Time(8,30),new Time(9,50)),
                            }),
                    }
                },
                new Class("MAT2040", "another one")
                {
                    lectures = new List<LectureSession>
                    {
                        new LectureSession
                            ("L01","1515","staff",
                            new List<SessionTime>
                            {
                                new SessionTime(DayOfWeek.Monday,new Time(8,30),new Time(9,50)),
                                new SessionTime(DayOfWeek.Wednesday,new Time(8,30),new Time(9,50)),
                            }),
                        new LectureSession
                            ("L01Copy","1515Copy","staff",
                            new List<SessionTime>
                            {
                                new SessionTime(DayOfWeek.Monday,new Time(8,30),new Time(9,50)),
                                new SessionTime(DayOfWeek.Wednesday,new Time(8,30),new Time(9,50)),
                            }),
                    }
                },
                new Class("ECO3121", "the third one")
                {
                    lectures = new List<LectureSession>
                    {
                        new LectureSession
                            ("L01","1437","staff",
                            new List<SessionTime>
                            {
                                new SessionTime(DayOfWeek.Tuesday,new Time(13,30),new Time(14,50)),
                                new SessionTime(DayOfWeek.Thursday,new Time(13,30),new Time(14,50)),
                            }),
                        new LectureSession
                            ("L01Copy","1437Copy","staff",
                            new List<SessionTime>
                            {
                                new SessionTime(DayOfWeek.Tuesday,new Time(13,30),new Time(14,50)),
                                new SessionTime(DayOfWeek.Thursday,new Time(13,30),new Time(14,50)),
                            }),
                    }
                },
                new Class("GFH1000", "the fourth one")
                {
                    lectures = new List<LectureSession>
                    {
                        new LectureSession
                            ("L01","1210","staff",
                            new List<SessionTime>
                            {
                                new SessionTime(DayOfWeek.Friday,new Time(13,00),new Time(13,50)),
                            }),
                    }
                },
            };

            #endregion


            // TODO: Retrieve class data from OCR outcome.

            // Generate all possible solutions.
            var weightedAllClasses = allClasses.OrderByDescending(c => c.weight).ToList();

            ClassSelector.FindSchedules(weightedAllClasses).ForEach(Console.WriteLine);


            var tmp = ClassSelector.Enroll3(weightedAllClasses, new Schedule());

            // TODO: Something else.
        }

    }
}
