using System;
using System.Collections.Generic;
using System.Text;

namespace AutoSchedule
{
    class Class
    {
        public readonly string code;

        public readonly string teacher;

        public List<Session> lectures;

        public List<Session> tutorials;

        public Class(string code, string teacher)
        {
            this.code = code;
            this.teacher = teacher;
        }
    }
}
