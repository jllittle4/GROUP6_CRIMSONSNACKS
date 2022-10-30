using api.Models;

namespace api.Interfaces
{
    public interface IFindRecentTimeEvent
    {
        public TimeEvent FindTimeEvent();
    }
}