using System;
using System.Collections.Generic;
using System.Text;

namespace AutoSchedule
{
    public enum SessionType
    {
        Lecture = 0,
        Tutorial = 1,
    }

    /// <summary>
    /// A base class of actual class sessions.
    /// </summary>
    [Serializable]
    public class Session
    {
        public readonly SessionType sessionType;

        /// <summary>
        /// Represents all time of the session. 
        /// </summary>
        /// <remarks>E.g. Mon 8:30-10:20 and Wed 8:30-10:20.</remarks>
        public List<SessionTime> sessionTimes;

        public readonly string instructor;

        public readonly string code;

        public readonly string name;

        public Session(SessionType sessionType, string name, string code, string instructor, 
                       List<SessionTime> sessionTimes)
        {
            this.sessionType = sessionType;
            this.name = name;
            this.code = code;
            this.instructor = instructor;
            this.sessionTimes = sessionTimes;
        }

        /// <summary>
        /// Judge whether a new session will have conflict with original sessions.
        /// </summary>
        /// <param name="session2">The new session.</param>
        /// <returns>true if there is time conflict, false otherwise.</returns>
        public bool HasConflictSession(Session session2)
        {
            foreach (var sessionTime in sessionTimes)
            {
                foreach (var otherSessionTime in session2.sessionTimes)
                {
                    if (sessionTime.ConflictWith(otherSessionTime)) return true;
                }
            }

            return false;
        }
    }
}
