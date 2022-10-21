using api.Models;

namespace api.Interfaces
{
    public interface IUpdateOneTimeEvent
    {
        public void UpdateOneTimeEvent(int id, TimeEvent myTimeEvent);
    }
}