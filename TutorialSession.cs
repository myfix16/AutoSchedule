using System;
using System.Collections.Generic;
using System.Text;

namespace AutoSchedule
{
    /// <summary>
    /// Actual tutorial session.
    /// </summary>
    internal class TutorialSession : Session
    {
        public TutorialSession(string code, string instructor, List<SessionTime> classTimes)
            : base(SessionType.Tutorial, code, instructor, classTimes)
        {
            throw new System.NotImplementedException();
        }
    }
}