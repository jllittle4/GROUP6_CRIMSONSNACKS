using api.Models;

namespace api.Interfaces
{
    public interface IReadOneTimeEvent
    {
        public TimeEvent ReadOneTimeEvent(int id);
    }
}