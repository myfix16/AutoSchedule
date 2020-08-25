using System;
using System.Collections.Generic;
using System.Text;

namespace AutoSchedule
{
    /// <summary>
    /// A schedule that contains all class selected.
    /// </summary>
    internal class Schedule
    {
        public List<Session> sessions;

        public Schedule()
        {
            sessions = new List<Session>();
        }

        /// <summary>
        /// Validate whether one session can be successfully added. 
        /// That is, whether there is any time conflict of sessions.
        /// </summary>
        /// <param name="newClass"></param>
        /// <returns></returns>
        public bool Validate(Session newSession)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a new session into the schedule.
        /// </summary>
        /// <returns>Whether add is successful.</returns>
        public bool AddSession(Session newSession)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Drop a session from the schedule.
        /// </summary>
        /// <param name="className"></param>
        /// <returns>Whether drop is successful.</returns>
        public bool DropSession(string sessionName)
        {
            throw new NotImplementedException();
        }
    }
}
