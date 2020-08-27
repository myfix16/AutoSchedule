using System;
using System.Collections.Generic;

namespace AutoSchedule
{
    /// <summary>
    /// Actual lecture session.
    /// </summary>
    [Serializable]
    public class LectureSession : Session, IContainsSessions<TutorialSession, LectureSession>
    {
        public List<TutorialSession> SubSessions { get; set; }

        public LectureSession(string name, string code, string instructor, List<SessionTime> sessionTimes)
            : base(SessionType.Lecture, name, code, instructor, sessionTimes)
        {

        }

        public bool Validate(TutorialSession newSession)
        {
            throw new NotImplementedException();
        }

        public LectureSession WithAdded(TutorialSession element)
        {
            throw new NotImplementedException();
        }

    }
}