using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace AutoSchedule
{
    [Serializable]
    public struct Time : IComparable<Time>, IEquatable<Time>
    {
        public readonly int hour;

        public readonly int minute;

        /// <summary>
        /// Represents the time span from 00:00 to this time counted in minutes.
        /// </summary>
        public readonly int totalMinutes;

        public Time(int hour, int minute)
        {
            if (hour < 0 || hour > 23)
                throw new ArgumentOutOfRangeException("The number of hour must be within [0,23].");

            if (minute < 0 || minute > 59)
                throw new ArgumentOutOfRangeException("The number of minute must be within [0,59].");

            this.hour = hour;
            this.minute = minute;
            totalMinutes = hour * 60 + minute;
        }

        public int CompareTo([AllowNull] Time other) => totalMinutes - other.totalMinutes;

        public bool Equals([AllowNull] Time other) => totalMinutes == other.totalMinutes;

        #region Operators

        public static bool operator >(Time t1, Time t2) => t1.totalMinutes > t2.totalMinutes;

        public static bool operator <(Time t1, Time t2) => t1.totalMinutes < t2.totalMinutes;

        public static bool operator <=(Time t1, Time t2) => t1.totalMinutes >= t2.totalMinutes;

        public static bool operator >=(Time t1, Time t2) => t1.totalMinutes >= t2.totalMinutes;

        #endregion

    }
}