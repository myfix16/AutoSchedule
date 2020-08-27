using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AutoSchedule
{
    [Serializable]
    static class ClassSelector
    {
        public static List<Schedule> FindSchedules(IEnumerable<IEnumerable<Session>> sessionContainer)
        {
            var outcome = new List<Schedule>();

            // Inner function that finds all suitable schedules.
            void Enroll(IEnumerable<IEnumerable<Session>> sessionContainer, Schedule currentScheme)
            {
                if (sessionContainer.Count() == 0)
                {
                    outcome.Add(currentScheme);
                }
                else
                {
                    foreach (var session in sessionContainer.First().Where(l => currentScheme.Validate(l)))
                    {
                        Enroll(sessionContainer.Skip(1), currentScheme.WithAdded(session));
                    }
                }
            }

            Enroll(sessionContainer, new Schedule());
            
            return outcome;
        }
    }
}
