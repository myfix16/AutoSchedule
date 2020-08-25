﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AutoSchedule
{
    /// <summary>
    /// Represents a macro class like CSC1001.
    /// </summary>
    internal class Class
    {
        public readonly string code;

        public readonly string teacher;

        public List<LectureSession> lectures;

        public List<TutorialSession> tutorials;

        /// <summary>
        /// The weight of one class. It decides the priority of one class in enrollment.
        /// </summary>
        public int weight;

        public Class(string code, string teacher, int weight = 1)
        {
            this.code = code;
            this.teacher = teacher;
            this.weight = weight;
        }
    }
}
