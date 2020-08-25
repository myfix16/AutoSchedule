using System;
using System.Collections.Generic;
using System.Text;

namespace AutoSchedule
{
    /// <summary>
    /// Actual lecture session.
    /// </summary>
    [Serializable]
    internal class LectureSession : Session
    {
        public LectureSession(string name, string code, string instructor, List<SessionTime> sessionTimes)
            : base(SessionType.Lecture, name, code, instructor, sessionTimes)
        {
            
        }
    }
}