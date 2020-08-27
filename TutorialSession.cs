﻿using System;
using System.Collections.Generic;

namespace AutoSchedule
{
    /// <summary>
    /// Actual tutorial session.
    /// </summary>
    [Serializable]
    public class TutorialSession : Session
    {
        public TutorialSession(string name, string code, string instructor, List<SessionTime> sessionTimes)
            : base(SessionType.Tutorial, name, code, instructor, sessionTimes)
        {

        }
    }
}