using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoSchedule
{
    [Serializable]
    static class ClassSelector
    {
        /// <summary>
        /// Find all possible class schedules.
        /// </summary>
        /// <param name="allClasses"></param>
        /// <param name="currentSchedule"></param>
        /// <returns>A list of all schedules.</returns>
        //public static List<Schedule> FindLectureSchedules(IEnumerable<Class> allClasses)
        //{
        //    var outcome = new List<Schedule>();

        //    void Enroll(IEnumerable<Class> allClasses, Schedule currentSchedule)
        //    {
        //        if (allClasses.Count() == 0)
        //        {
        //            outcome.Add(currentSchedule);
        //        }
        //        else
        //        {
        //            foreach (var session in allClasses.First().lectures.Where(l => currentSchedule.Validate(l)))
        //            {
        //                Enroll(allClasses.Skip(1), currentSchedule.WithAdded(session));
        //            }
        //        }
        //    }

        //    Enroll(allClasses, new Schedule());

        //    return outcome;
        //}

        private static List<Schedule> FindSchedules<TSession, TContainer>(IEnumerable<TContainer> sessionContainer)

            where TContainer : IContainsSessions<TSession, TContainer>
            where TSession : Session
        {
            var outcome = new List<Schedule>();

            // Inner function that finds all suitable schedules.
            void Enroll(IEnumerable<TContainer> sessionContainer, Schedule currentOutcome)
            {
                if (sessionContainer.Count() == 0)
                {
                    outcome.Add(currentOutcome);
                }
                else
                {
                    foreach (var session in sessionContainer.First().SubSessions
                        .Where(l => currentOutcome.Validate(l)))
                    {
                        Enroll(sessionContainer.Skip(1), currentOutcome.WithAdded(session));
                    }
                }
            }

            Enroll(sessionContainer, new Schedule());

            return outcome;
        }

        public static List<Schedule> FindLectures(IEnumerable<Class> allClasses)
            => FindSchedules<LectureSession, Class>(allClasses);

        public static IEnumerable<List<Schedule>> FindTutorials(IEnumerable<Schedule> allSchedules)
        {
            foreach (var schedule in allSchedules)
            {
                var lectureCollections = new List<LectureSession>();

                foreach (var item in schedule.SubSessions)
                {
                    lectureCollections.Add(item as LectureSession);
                }

                yield return FindSchedules<TutorialSession, LectureSession>(lectureCollections);
            }
        }
    }
}
