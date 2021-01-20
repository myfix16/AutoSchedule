using System;

namespace AutoSchedule.Core.Models
{
    // TODO: Check whether there is an option to migrate to DateTime class.

    /// <summary>
    /// Represents the time of one session.
    /// </summary>
    [Serializable]
    public record SessionTime
    {
        public DayOfWeek DayOfWeek { get; init; }

        public Time StartTime { get; init; }

        public Time EndTime { get; init; }

        // Using delta time from Monday has problem here since Sunday is the first day in enum.
        // However, it doesn't affect the result because there is no class in the weekend.
        private int? startTimeFromMon;
        /// <summary>
        /// Start time counting from 00:00 Mon.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public int StartTimeFromMon
        {
            get
            {
                startTimeFromMon ??= StartTime.TotalMinutes + ((int)DayOfWeek - 1) * 24 * 60;
                return startTimeFromMon.Value;
            }
            private set { startTimeFromMon = value; }
        }

        private int? endTimeFromMon;
        /// <summary>
        /// End time counting from 00:00 Mon.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public int EndTimeFromMon
        {
            get
            {
                endTimeFromMon ??= EndTime.TotalMinutes + ((int)DayOfWeek - 1) * 24 * 60;
                return endTimeFromMon.Value;
            }
            private set { endTimeFromMon = value; }
        }

        public SessionTime()
        {
            DayOfWeek = DayOfWeek.Monday;
            StartTime = new Time();
            EndTime = new Time();
        }

        [System.Text.Json.Serialization.JsonConstructor]
        [Newtonsoft.Json.JsonConstructor]
        public SessionTime(DayOfWeek dayOfWeek, Time startTime, Time endTime)
        {
            if (endTime < startTime)
                throw new ArgumentOutOfRangeException(nameof(endTime), "End time of class cannot be earlier than start time.");

            DayOfWeek = dayOfWeek;
            StartTime = startTime;
            EndTime = endTime;
        }

        public bool ConflictWith(SessionTime sessionTime2)
            => !(StartTimeFromMon > sessionTime2.EndTimeFromMon
                 || EndTimeFromMon < sessionTime2.StartTimeFromMon);

        public override string ToString()
            => $"{DayOfWeek} {StartTime}-{EndTime}";
    }
}