using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AutoSchedule
{
    [Serializable]
    static class ClassSelector
    {
#nullable enable
        private static List<Schedule> FindSchedules<TSession, TContainer>
            (IEnumerable<TContainer> sessionContainer, bool selectingTut = false)

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

            Enroll(sessionContainer, new Schedule(selectingTut ? sessionContainer : null));

            return outcome;
        }

        public static List<Schedule> FindLectures(IEnumerable<Class> allClasses)
            => FindSchedules<LectureSession, Class>(allClasses);

        public static IEnumerable<List<Schedule>> FindTutorials(IEnumerable<Schedule> allSchedules)
        {
            foreach (var schedule in allSchedules)
            {
                // Revert Session into LectureSession.
                // ? There might be some cooler method.
                var lectureCollections = new List<LectureSession>();

                foreach (var item in schedule.SubSessions)
                {
                    //lectureCollections.Add(item as LectureSession);
                    lectureCollections.Add((LectureSession)item);
                }

                yield return FindSchedules<TutorialSession, LectureSession>(lectureCollections);
            }
        }
    }
}
