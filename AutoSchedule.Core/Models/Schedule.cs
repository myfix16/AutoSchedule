using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AutoSchedule.Core.Models
{
    /// <summary>
    /// A schedule that contains all class selected.
    /// </summary>
    [Serializable]
    public class Schedule : ICopyable<Schedule>
    {
        public string Id = "1";

        public ObservableCollection<Session> Sessions = new();

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

        public Schedule WithAdded(Session element)
        {
            var newSchedule = ShallowCopy();
            newSchedule.Sessions.Add(element);
            return newSchedule;
        }

        public Schedule ShallowCopy() => new()
        {
            Id = this.Id,
            Sessions = new ObservableCollection<Session>(this.Sessions),
        };

        [Obsolete("Deep copy is not available.")]
        public Schedule DeepCopy()
        {
            throw new NotImplementedException("Deep copy is not available.");
        }
    }
}
