using api.Models;

namespace api.Interfaces
{
    public interface IFindTimeEventByDate
    {
        public List<TimeEvent> Find(Request myRequest);
    }
}