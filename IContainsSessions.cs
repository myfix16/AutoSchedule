using System;
using System.Collections.Generic;
using System.Text;

namespace AutoSchedule
{
    public interface IContainsSessions<TSession, out TResult>
        where TSession : Session
    {
        public List<TSession> SubSessions { get; set; }

        public TResult WithAdded(TSession element);

        public bool Validate(TSession newSession);
    }
}
