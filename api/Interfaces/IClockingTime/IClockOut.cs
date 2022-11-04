using api.Models;

namespace api.Interfaces
{
    public interface IClockOut
    {
        public void ClockingOut(TimeEvent myTimeEvent);
    }
}