using System;

namespace AutoSchedule.Core.Models
{
    /// <summary>
    /// Represents the time of one session.
    /// </summary>
    [Serializable]
    public struct SessionTime
    {
        [System.Text.Json.Serialization.JsonInclude]
        [Newtonsoft.Json.JsonRequired]
        public DayOfWeek DayOfWeek;

        [System.Text.Json.Serialization.JsonInclude]
        [Newtonsoft.Json.JsonRequired]
        public Time StartTime;

        [System.Text.Json.Serialization.JsonInclude]
        [Newtonsoft.Json.JsonRequired]
        public Time EndTime;

        // Using delta time from Monday has problem here since Sunday is the first day in enum.
        // However, it doesn't affect the result because there is no class in the weekend.
        /// <summary>
        /// Start time counting from 00:00 Mon.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public int StartTimeFromMon;

        /// <summary>
        /// End time counting from 00:00 Mon.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public int EndTimeFromMon;
        
        [System.Text.Json.Serialization.JsonConstructor]
        [Newtonsoft.Json.JsonConstructor]
        public SessionTime(DayOfWeek dayOfWeek, Time startTime, Time endTime)
        {
            if (endTime < startTime)
                throw new ArgumentOutOfRangeException(nameof(endTime), "End time of class cannot be earlier than start time.");

            DayOfWeek = dayOfWeek;
            StartTime = startTime;
            EndTime = endTime;
            StartTimeFromMon = StartTime.TotalMinutes + ((int)DayOfWeek - 1) * 24 * 60;
            EndTimeFromMon = EndTime.TotalMinutes + ((int)DayOfWeek - 1) * 24 * 60;
        }

        public bool ConflictWith(SessionTime sessionTime2)
            => !(StartTimeFromMon > sessionTime2.EndTimeFromMon || EndTimeFromMon < sessionTime2.StartTimeFromMon);

        public override string ToString() => $"{DayOfWeek} {StartTime}-{EndTime}";
    }
}