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
    internal class Schedule : IContainsSessions<Session, Schedule>
    {
        public List<Session> SubSessions { get; set; }

        public SessionType CointainedSessionType { get; set; }

#nullable enable
        public Schedule(List<Session>? sessions)
        {
            SubSessions = sessions ?? new List<Session>();
        }

        /// <summary>
        /// Validate whether one session can be successfully added. 
        /// That is, whether there is any time conflict of sessions.
        /// </summary>
        /// <param name="newClass"></param>
        /// <returns></returns>
        public bool Validate(Session newSession)
        {
            foreach (var session in SubSessions)
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
        public void AddSession(Session newSession) => SubSessions.Add(newSession);

        public Schedule WithAdded(Session element)
        {
            var newSchedule = DeepCopySerialization.DeepCopy(this);
            newSchedule.AddSession(element);
            return newSchedule;
        }

        #region not used for now
        /// <summary>
        /// Drop a session from the schedule.
        /// </summary>
        /// <param name="sessionCode"></param>
        /// <returns>true if drop is successful, false otherwise.</returns>
        public bool DropSession(string sessionCode)
        {
            var sessionToDelete = SubSessions.Find(s => s.code == sessionCode);
            if (sessionToDelete == null) return false;
            else
            {
                SubSessions.Remove(sessionToDelete);
                return true;
            }
        }

        public bool DropSession(Session session) => SubSessions.Remove(session);

        public override string ToString()
        {
            var outcome = string.Empty;
            SubSessions.ForEach(s => outcome += (s.code + " "));
            return outcome;
        }
        #endregion

    }
}
