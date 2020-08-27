using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MyToolLibrary.Copy;

namespace AutoSchedule
{
    /// <summary>
    /// A schedule that contains all class selected.
    /// </summary>
    [Serializable]
    internal class Schedule
    {
        public List<Session> Lectures { get; set; } = new List<Session>();

        public List<Session> Tutorials { get; set; } = new List<Session>();

        /// <summary>
        /// Validate whether one session can be successfully added. 
        /// That is, whether there is any time conflict of sessions.
        /// </summary>
        /// <param name="newClass"></param>
        /// <returns></returns>
        public static bool Validate(Session newSession, IEnumerable<Session> existingSessions)
        {
            foreach (var session in existingSessions)
            {
                if (session.HasConflictSession(newSession))
                    return false;
            }

            return true;
        }

        public bool Validate(Session newSession)
        {
            foreach (var session in newSession.sessionType switch
                {
                    SessionType.Lecture => Lectures,
                    SessionType.Tutorial => Tutorials,
                    _ => throw new NotImplementedException()
                })

            {
                if (session.HasConflictSession(newSession))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Add a new session into the schedule.
        /// </summary>
        /// <returns>Whether add is successful.</returns>
        public static void AddSession(Session newSession, List<Session> existingSessions)
            => existingSessions.Add(newSession);

        public void AddSession(Session newSession)
        {
            var target = newSession.sessionType switch
            {
                SessionType.Lecture => Lectures,
                SessionType.Tutorial => Tutorials,
                _ => throw new NotImplementedException()
            };

            target.Add(newSession);
        }

        public Schedule WithAdded(Session element)
        {
            var newSchedule = DeepCopySerialization.DeepCopy(this);
            newSchedule.AddSession(element);
            return newSchedule;
        }
    }
}
