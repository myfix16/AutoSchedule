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
    internal class Session
    {
        public readonly SessionType sessionType;

        public List<SessionTime> classTimes;

        public readonly string instructor;

        public readonly string code;

        public Session(
            SessionType sessionType, string code, string instructor, List<SessionTime> sessionTimes)
        {
            this.sessionType = sessionType;
            this.code = code;
            this.instructor = instructor;
            this.classTimes = sessionTimes;
        }
    }
}
