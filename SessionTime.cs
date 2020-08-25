using System;
using System.Collections.Generic;
using System.Text;

namespace AutoSchedule
{
    /// <summary>
    /// Represents the time of one session.
    /// </summary>
    public struct SessionTime
    {
        public DayOfWeek DayOfWeek { get; }

        public Time StartTime { get; }

        public Time EndTime { get; }

        public SessionTime(DayOfWeek dayOfWeek, Time startTime, Time endTime)
        {
            if (endTime < startTime)
            {
                throw new InvalidTimeException("End time of class cannot be earlier than start time.");
            }

            DayOfWeek = dayOfWeek;
            StartTime = startTime;
            EndTime = endTime;
        }

        #region We only need one of the following methods. Choose the one that is easy to use.

        public static bool HasConflict(SessionTime classTime1, SessionTime classTime2)
        {
            if (classTime1.DayOfWeek != classTime2.DayOfWeek) return false;

            // By compliment set.
            return !(classTime1.StartTime > classTime2.EndTime || classTime1.EndTime < classTime2.StartTime);
        }

        public bool HasConflict(SessionTime classTime2)
        {
            if (DayOfWeek != classTime2.DayOfWeek) return false;

            // By compliment set.
            return !(StartTime > classTime2.EndTime || EndTime < classTime2.StartTime);
        }

        #endregion
    }
}