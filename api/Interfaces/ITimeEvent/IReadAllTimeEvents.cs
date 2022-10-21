using api.Models;

namespace api.Interfaces
{
    public interface IReadAllTimeEvents
    {
        public List<TimeEvent> ReadAllTimeEvents();
    }
}