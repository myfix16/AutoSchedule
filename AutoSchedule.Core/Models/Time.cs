﻿using System;

namespace AutoSchedule.Core.Models
{
    [Serializable]
    public struct Time : IComparable<Time>, IEquatable<Time>
    {
        [System.Text.Json.Serialization.JsonInclude]
        [Newtonsoft.Json.JsonRequired]
        public int Hour;

        [System.Text.Json.Serialization.JsonInclude]
        [Newtonsoft.Json.JsonRequired]
        public int Minute;

        /// <summary>
        /// Represents the time span from 00:00 to this time counted in minutes.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public readonly int TotalMinutes;

        /// <summary>
        /// Construct the time by a string.
        /// </summary>
        /// <param name="timeString">A string representation of time, for example, 10:30.</param>
        public Time(string timeString)
        {
            var splittedString = timeString.Replace(" ", string.Empty).Split(':');
            Hour = int.Parse(splittedString[0]);
            Minute = int.Parse(splittedString[1]);
            TotalMinutes = (Hour * 60) + Minute;
        }

        [System.Text.Json.Serialization.JsonConstructor]
        [Newtonsoft.Json.JsonConstructor]
        public Time(int hour, int minute)
        {
            if (hour is < 0 or > 23)
                throw new ArgumentOutOfRangeException(nameof(hour), "The number of hour must be within [0,23].");

            if (minute is < 0 or > 59)
                throw new ArgumentOutOfRangeException(nameof(minute), "The number of minute must be within [0,59].");

            Hour = hour;
            Minute = minute;
            TotalMinutes = (Hour * 60) + Minute;
        }

        public int CompareTo(Time other) => TotalMinutes - other.TotalMinutes;

        public bool Equals(Time other) => TotalMinutes == other.TotalMinutes;

        public override int GetHashCode() => TotalMinutes.GetHashCode();

        public override string ToString() => $"{Hour}:{(Minute == 0 ? "00" : Minute)}";

        #region Operators

        public static bool operator >(Time t1, Time t2) => t1.TotalMinutes > t2.TotalMinutes;

        public static bool operator <(Time t1, Time t2) => t1.TotalMinutes < t2.TotalMinutes;

        public static bool operator <=(Time t1, Time t2) => t1.TotalMinutes >= t2.TotalMinutes;

        public static bool operator >=(Time t1, Time t2) => t1.TotalMinutes >= t2.TotalMinutes;

        public static bool operator ==(Time t1, Time t2) => t1.TotalMinutes == t2.TotalMinutes;

        public static bool operator !=(Time t1, Time t2) => t1.TotalMinutes != t2.TotalMinutes;

        #endregion
    }
}