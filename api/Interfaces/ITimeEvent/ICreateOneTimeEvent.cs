using api.Models;

namespace api.Interfaces
{
    public interface ICreateOneTimeEvent
    {
        public void CreateOneTimeEvent(TimeEvent myTimeEvent);
    }
}