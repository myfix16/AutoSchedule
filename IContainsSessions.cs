using System.Collections.Generic;

namespace AutoSchedule
{
    public interface IContainsSessions<TSession, out TContainer>
        where TSession : Session
    {
        public List<TSession> SubSessions { get; set; }

        //public TContainer WithAdded(TSession element);

        //public bool Validate(TSession newSession);
    }
}
