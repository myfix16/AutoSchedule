using System;
using System.Collections.Generic;
using System.Text;

namespace AutoSchedule.Core.Models
{
    public record SingleSession
    {
        public string SessionType { get; init; }

        public SessionTime SessionTime { get; init; }

        public string Instructor { get; init; }

        public string Code { get; init; }

        public string Name { get; init; }

        public SingleSession(string sessionType, string name, string code, 
            string instructor, SessionTime sessionTime)
        {
            SessionType = sessionType;
            Name = name;
            Code = code;
            Instructor = instructor;
            SessionTime = sessionTime;
        }
    }
}
