using System;

namespace AutoSchedule
{
    public class InvalidTimeException : Exception
    {
        public InvalidTimeException() { }

        public InvalidTimeException(string message) : base(message) { }
    }
}
