using System;
using System.Collections.Generic;
using System.Text;

namespace AutoSchedule
{
    /// <summary>
    /// Actual lecture session.
    /// </summary>
    internal class LectureSession : Session
    {
        public LectureSession(string code, string instructor, List<SessionTime> sessionTimes)
            : base(SessionType.Lecture, code, instructor, sessionTimes)
        {
            throw new System.NotImplementedException();
        }
    }
}