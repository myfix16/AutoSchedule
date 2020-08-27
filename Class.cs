using System;
using System.Collections.Generic;

namespace AutoSchedule
{
    /// <summary>
    /// Represents a macro class like CSC1001.
    /// </summary>
    [Serializable]
    public class Class : IContainsSessions<LectureSession, Class>
    {
        public readonly string name;

        public readonly string teacher;

        public List<LectureSession> SubSessions { get; set; }

        /// <summary>
        /// The weight of one class. It decides the priority of one class in enrollment.
        /// </summary>
        public int weight;

        public Class(string name, string teacher, int weight = 1)
        {
            this.name = name;
            this.teacher = teacher;
            this.weight = weight;
        }

        public Class WithAdded(LectureSession element)
        {
            throw new NotImplementedException();
        }

        public bool Validate(LectureSession newSession)
        {
            throw new NotImplementedException();
        }
    }
}
