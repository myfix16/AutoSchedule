using System;
using System.Collections.Generic;
using System.Text;
using MyToolLibrary.Copy;

namespace AutoSchedule
{
    /// <summary>
    /// A schedule that contains all class selected.
    /// </summary>
    [Serializable]
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
            foreach (var session in sessions)
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
        public void AddSession(Session newSession) => sessions.Add(newSession);

        public Schedule WithAdded(Session newSession)
        {
            var newSchedule = DeepCopySerialization.DeepCopy(this);
            newSchedule.AddSession(newSession);
            return newSchedule;
        }

        /// <summary>
        /// Drop a session from the schedule.
        /// </summary>
        /// <param name="sessionCode"></param>
        /// <returns>true if drop is successful, false otherwise.</returns>
        public bool DropSession(string sessionCode)
        {
            var sessionToDelete = sessions.Find(s => s.code == sessionCode);
            if (sessionToDelete == null) return false;
            else
            {
                sessions.Remove(sessionToDelete);
                return true;
            }
        }

        public bool DropSession(Session session) => sessions.Remove(session);

        public override string ToString()
        {
            var outcome = string.Empty;
            sessions.ForEach(s => outcome += (s.code + " "));
            return outcome;
        }
    }
}
