using System.Collections.Generic;
using System.Threading.Tasks;
using AutoSchedule.Core.Models;

namespace AutoSchedule.Core.Helpers
{
    /// <summary>
    /// Define a set of methods that can retrieve session info from various data source.
    /// </summary>
    public interface IDataProvider
    {
        public Task<IEnumerable<Session>> GetSessionsAsync();

        public IEnumerable<Session> GetSessions();
    }
}
