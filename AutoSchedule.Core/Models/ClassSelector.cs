using System.Collections.Generic;
using System.Linq;

namespace AutoSchedule.Core.Models
{
    public static class ClassSelector
    {
        public static List<Schedule> FindSchedules(IEnumerable<IEnumerable<Session>> sessionContainer)
        {
            int id = 0;
            var outcome = new List<Schedule>();

            // Inner function that finds all suitable schedules.
            void Enroll(IEnumerable<IEnumerable<Session>> sessions, Schedule currentScheme, int maxSchedules=15)
            {
                if (id >= maxSchedules) return;

                if (!sessions.Any())
                {
                    outcome.Add(currentScheme);
                    currentScheme.Id = (++id).ToString();
                }
                else
                {
                    foreach (var session in sessions.First().Where(l => currentScheme.Validate(l)))
                    {
                        Enroll(sessions.Skip(1), currentScheme.WithAdded(session));
                    }
                }
            }

            Enroll(sessionContainer, new Schedule { Id = id.ToString() });

            return outcome;
        }
    }
}
