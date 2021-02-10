using System.Threading.Tasks;

namespace AutoSchedule.Core.Helpers
{
    /// <summary>
    /// Define a set of methods that can retrieve session info from various data source.
    /// </summary>
    public interface IDataProvider<T>
    {
        public Task<T> GetSessionsAsync();

        public T GetSessions();
    }
}
