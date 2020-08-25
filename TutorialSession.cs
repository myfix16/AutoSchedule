using System;
using System.Collections.Generic;
using System.Text;

namespace AutoSchedule
{
    /// <summary>
    /// Actual tutorial session.
    /// </summary>
    [Serializable]
    internal class TutorialSession : Session
    {
        public TutorialSession(string name, string code, string instructor, List<SessionTime> sessionTimes)
            : base(SessionType.Tutorial, name, code, instructor, sessionTimes)
        {
            
        }
    }
}