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

            List<Class> allClasses = new List<Class>
            {
                new Class("FIN2020", "someone")
                {
                    SubSessions = new List<LectureSession>
                    {
                        new LectureSession
                            ("L01","1841","staff",
                            new List<SessionTime>
                            {
                                new SessionTime(DayOfWeek.Tuesday,new Time(8,30),new Time(9,50)),
                                new SessionTime(DayOfWeek.Thursday,new Time(8,30),new Time(9,50)),
                            })
                        {
                            SubSessions=new List<TutorialSession>
                            {
                                new TutorialSession
                                    ("T01","1843","staff",
                                    new List<SessionTime>
                                    {
                                        new SessionTime(DayOfWeek.Wednesday,new Time(18,00),new Time(18,50))
                                    })
                            }
                        },
                    }
                },
                new Class("MAT2040", "another one")
                {
                    SubSessions = new List<LectureSession>
                    {
                        new LectureSession
                            ("L01","1515","staff",
                            new List<SessionTime>
                            {
                                new SessionTime(DayOfWeek.Monday,new Time(8,30),new Time(9,50)),
                                new SessionTime(DayOfWeek.Wednesday,new Time(8,30),new Time(9,50)),
                            })
                        {
                            SubSessions=new List<TutorialSession>
                            {
                                new TutorialSession
                                    ("T01","1519","staff",
                                    new List<SessionTime>
                                    {
                                        new SessionTime(DayOfWeek.Monday,new Time(19,00),new Time(19,50))
                                    })
                            }
                        },
                    }
                },
                new Class("ECO3121", "the third one")
                {
                    SubSessions = new List<LectureSession>
                    {
                        new LectureSession
                            ("L01","1437","staff",
                            new List<SessionTime>
                            {
                                new SessionTime(DayOfWeek.Tuesday,new Time(13,30),new Time(14,50)),
                                new SessionTime(DayOfWeek.Thursday,new Time(13,30),new Time(14,50)),
                            })
                        {
                            SubSessions =new List<TutorialSession>
                            {
                                new TutorialSession
                                    ("T01","1439","staff",
                                    new List<SessionTime>
                                    {
                                        new SessionTime(DayOfWeek.Tuesday,new Time(20,00),new Time(20,50))
                                    })
                            }
                        },
                    }
                },
                new Class("GFH1000", "the fourth one")
                {
                    SubSessions = new List<LectureSession>
                    {
                        new LectureSession
                            ("L01","1210","staff",
                            new List<SessionTime>
                            {
                                new SessionTime(DayOfWeek.Friday,new Time(13,00),new Time(13,50)),
                            })
                        {
                            SubSessions = new List<TutorialSession>
                            {
                                new TutorialSession
                                    ("T14","1232","staff",
                                    new List<SessionTime>
                                    {
                                        new SessionTime(DayOfWeek.Monday,new Time(10,30),new Time(12,20))
                                    }),
                            }
                        },
                    }
                },
            };

            #endregion


            // TODO: Retrieve class data from OCR outcome.

            // Generate all possible solutions.
            var weightedAllClasses = allClasses.OrderByDescending(c => c.weight).ToList();

            Console.WriteLine("Lectures:");

            var tutorials =
            ClassSelector.FindLectures(weightedAllClasses)
                         .ForEachObject(Console.WriteLine)
                         .Map(ClassSelector.FindTutorials)
                         .ToList();

            Console.WriteLine();
            Console.WriteLine("Tutorials:");

            foreach (var solutionForOneLectureCombination in tutorials)
            {
                Console.WriteLine("****************");
                solutionForOneLectureCombination.ForEach(Console.WriteLine);
                Console.WriteLine("****************");
            }

            Console.WriteLine("Hello World!");

            // TODO: Something else.
        }

    }
}
