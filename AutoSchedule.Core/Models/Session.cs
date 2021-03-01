using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

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

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public string SessionTimesString
        {
            get 
            { 
                string output = ""; 
                foreach (var item in SessionTimes) 
                    output += $" {item};";
                return output.Trim();
            }
        }

        public string Instructor { get; init; }

        [JsonPropertyName("id")]
        [JsonProperty(propertyName: "id")]
        public string Code { get; init; }

        public string Name { get; init; }

        private Session()
        {

        }

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
            foreach (var item in SessionTimes) output += $" {item}";
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

        public string GetClassifiedName() => $"{Name.Split(' ')[0]} {SessionType}";
    }
}
