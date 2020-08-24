using System;
using System.Collections.Generic;
using System.Text;

namespace AutoSchedule
{
    public enum SessionType
    {
        Lecture,
        Tutorial,
    }

    class Session
    {
        public readonly SessionType sessionType;

        public readonly DateTime time;

        public readonly string instructor;

        public readonly string code;

        public Session(SessionType sessionType, string code, string instructor, DateTime time)
        {
            this.sessionType = sessionType;
            this.code = code;
            this.instructor = instructor;
            this.time = time;
        }
    }
}
