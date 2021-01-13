using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AutoSchedule.Core.Models
{
    [Serializable]
    public sealed record Time : IComparable<Time>, IEquatable<Time>
    {
        public int Hour { get; init; }

        public int Minute { get; init; }

        private int? totalMinutes;

        /// <summary>
        /// Represents the time span from 00:00 to this time counted in minutes.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public int TotalMinutes
        {
            get { totalMinutes ??= (Hour * 60) + Minute; return totalMinutes.Value; }
            private set => totalMinutes = value;
        }

        public Time()
        {
            Hour = 0;
            Minute = 0;
        }

        /// <summary>
        /// Construct the time by a string.
        /// </summary>
        /// <param name="timeString">A string  representation of time, for example, 10:30.</param>
        public Time(string timeString)
        {
            var splittedString = timeString.Replace(" ", string.Empty).Split(':');
            Hour = int.Parse(splittedString[0]);
            Minute = int.Parse(splittedString[1]);
        }

        [System.Text.Json.Serialization.JsonConstructor]
        [Newtonsoft.Json.JsonConstructor]
        public Time(int hour, int minute)
        {
            if (hour < 0 || hour > 23)
                throw new ArgumentOutOfRangeException(nameof(hour), "The number of hour must be within [0,23].");

            if (minute < 0 || minute > 59)
                throw new ArgumentOutOfRangeException(nameof(minute), "The number of minute must be within [0,59].");

            Hour = hour;
            Minute = minute;
        }

        public int CompareTo(Time other) => TotalMinutes - other.TotalMinutes;

        public bool Equals(Time other) => TotalMinutes == other.TotalMinutes;

        public override string ToString() => $"{Hour}:{(Minute == 0 ? "00" : Minute)}";

        #region Operators

        public static bool operator >(Time t1, Time t2) => t1.TotalMinutes > t2.TotalMinutes;

        public static bool operator <(Time t1, Time t2) => t1.TotalMinutes < t2.TotalMinutes;

        public static bool operator <=(Time t1, Time t2) => t1.TotalMinutes >= t2.TotalMinutes;

        public static bool operator >=(Time t1, Time t2) => t1.TotalMinutes >= t2.TotalMinutes;

        #endregion

    }
}