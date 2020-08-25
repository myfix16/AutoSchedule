using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoSchedule
{
    //[Serializable]
    static class ClassSelector
    {
        //? In fact, the return of this function is an Iterator containing nothing.
        #region Some strange things with yield return
        public static IEnumerable<Schedule> LazyEnroll(IEnumerable<Class> allClasses, Schedule currentSchedule)
        {
            throw new NotImplementedException();

            if (allClasses.Count() == 0)
            {
                Console.WriteLine("****************" + currentSchedule.ToString());
                yield return currentSchedule;
            }

            else
            {
                foreach (var session in allClasses.First().lectures.Where(l => currentSchedule.Validate(l)))
                {
                    currentSchedule.AddSession(session);

                    foreach (var inner in LazyEnroll(allClasses.Skip(1), currentSchedule))
                    {
                        //Console.WriteLine(times++);
                        //Console.WriteLine(inner);

                        yield return inner;
                    }

                    currentSchedule.DropSession(session);
                }

                //yield break;
            }
        }
        #endregion


        /// <summary>
        /// Find all possible class schedules.
        /// </summary>
        /// <param name="allClasses"></param>
        /// <param name="currentSchedule"></param>
        /// <returns>A list of all schedules.</returns>
        public static List<Schedule> FindSchedules(IEnumerable<Class> allClasses)
        {
            var outcome = new List<Schedule>();

            void Enroll(IEnumerable<Class> allClasses, Schedule currentSchedule)
            {
                if (allClasses.Count() == 0)
                {
                    outcome.Add(currentSchedule);
                }
                else
                {
                    foreach (var session in allClasses.First().lectures.Where(l => currentSchedule.Validate(l)))
                    {
                        Enroll(allClasses.Skip(1), currentSchedule.WithAdded(session));
                    }
                }
            }

            Enroll(allClasses, new Schedule());

            return outcome;
        }

        //public static List<Schedule> Enroll3(List<Class> allClasses, Schedule currentSchedule)
        //{
        //    var outcome = new List<Schedule>();

        //    if (allClasses.Count == 0)
        //    {
        //        //outcome.Add(currentSchedule);
        //        outcome.Add(currentSchedule);
        //    }
        //    else
        //    {

        //        var cls = allClasses[0];

        //        foreach (var session in cls.lectures.Where(l => currentSchedule.Validate(l)))
        //        {
        //            var returnedSchedule = Enroll3(allClasses.WithRemoved(cls), currentSchedule.WithAdded(session));
        //            if (returnedSchedule.Count != 0)
        //            {
        //                outcome.AddRange(returnedSchedule.ShallowCopy());
        //            }
        //        }
        //    }

        //    return outcome;
        //}
    }
}
