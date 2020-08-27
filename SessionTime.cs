using System;

namespace AutoSchedule
{
    /// <summary>
    /// Represents the time of one session.
    /// </summary>
    [Serializable]
    public struct SessionTime
    {
        public DayOfWeek DayOfWeek { get; }

        public Time StartTime { get; }

        public Time EndTime { get; }

        /// <summary>
        /// Start time counting from 00:00 Mon.
        /// </summary>
        public int StartTimeFromMon { get; }

        /// <summary>
        /// End time counting from 00:00 Mon.
        /// </summary>
        public int EndTimeFromMon { get; }

        public SessionTime(DayOfWeek dayOfWeek, Time startTime, Time endTime)
        {
            if (endTime < startTime)
                throw new ArgumentOutOfRangeException("End time of class cannot be earlier than start time.");

            DayOfWeek = dayOfWeek;
            StartTime = startTime;
            EndTime = endTime;

            StartTimeFromMon = startTime.totalMinutes + ((int)dayOfWeek - 1) * 24 * 60;
            EndTimeFromMon = endTime.totalMinutes + ((int)dayOfWeek - 1) * 24 * 60;
        }

        public bool ConflictWith(SessionTime sessionTime2)
            => !(StartTimeFromMon > sessionTime2.EndTimeFromMon
                 || EndTimeFromMon < sessionTime2.StartTimeFromMon);
    }
}