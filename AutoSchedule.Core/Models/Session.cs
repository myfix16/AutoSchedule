﻿using System;
using System.Collections.Generic;

namespace AutoSchedule.Core.Models
{
    /// <summary>
    /// A base class of actual class sessions.
    /// </summary>
    [Serializable]
    public record Session
    {
        public string SessionType { get; init; }

        /// <summary>
        /// Represents all time of the session. 
        /// </summary>
        /// <remarks>E.g. Mon 8:30-10:20 and Wed 8:30-10:20.</remarks>
        public List<SessionTime> SessionTimes { get; init; }

        public string Instructor { get; init; }

        public string Code { get; init; }

        public string Name { get; init; }

        public Session(string sessionType, string name, string code, string instructor,
                       List<SessionTime> sessionTimes)
        {
            SessionType = sessionType;
            Name = name;
            Code = code;
            Instructor = instructor;
            SessionTimes = sessionTimes;
        }

        public override string ToString()
        {
            var output = $"{Name} {Code} {Instructor}";
            foreach (var item in SessionTimes)
            {
                output += $" {item.DayOfWeek}{item.StartTime.Hour}:{item.StartTime.Minute}-" +
                    $"{item.DayOfWeek}{item.EndTime.Hour}:{item.EndTime.Minute}";
            }
            return output;
        }

        /// <summary>
        /// Judge whether a new session will have conflict with original sessions.
        /// </summary>
        /// <param name="session2">The new session.</param>
        /// <returns>true if there is time conflict, false otherwise.</returns>
        public bool HasConflictSession(Session session2)
        {
            foreach (var sessionTime in SessionTimes)
            {
                foreach (var otherSessionTime in session2.SessionTimes)
                {
                    if (sessionTime.ConflictWith(otherSessionTime)) return true;
                }
            }

            return false;
        }
    }
}