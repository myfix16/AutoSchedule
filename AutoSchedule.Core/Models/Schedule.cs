using System;
using System.Collections.Generic;

namespace AutoSchedule.Core.Models
{
    /// <summary>
    /// A schedule that contains all class selected.
    /// </summary>
    [Serializable]
    public class Schedule : ICopyable<Schedule>
    {
        public string ID { get; set; }
        public List<Session> Sessions { get; set; } = new List<Session>();

        /// <summary>
        /// Validate whether one session can be successfully added.
        /// That is, whether there is any time conflict of sessions.
        /// </summary>
        /// <param name="newSession"></param>
        /// <returns>true if newSession can be added, false otherwise.</returns>
        public bool Validate(Session newSession)
        {
            foreach (var session in Sessions)
            {
                if (session.HasConflictSession(newSession))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Add a new session into the schedule.
        /// </summary>
        /// <returns>true if add is successful, false otherwise</returns>
        public static void AddSession(Session newSession, List<Session> existingSessions)
            => existingSessions.Add(newSession);

        public void AddSession(Session newSession) => Sessions.Add(newSession);

        public Schedule WithAdded(Session element)
        {
            var newSchedule = ShallowCopy();
            newSchedule.AddSession(element);
            return newSchedule;
        }

        public Schedule ShallowCopy()
        {
            return new Schedule
            {
                ID = this.ID,
                Sessions = new List<Session>(this.Sessions),
            };
        }

        public Schedule DeepCopy()
        {
            throw new NotImplementedException();
        }
    }
}
