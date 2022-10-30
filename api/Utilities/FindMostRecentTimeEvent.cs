using api.Models;
using api.Controllers;
using api.Interfaces;
using api.CRUD;

namespace api.Utilities
{
    public class FindMostRecentTimeEvent
    {
        public TimeEvent myTimeEvent = new TimeEvent();

        public TimeEvent FindTimeEvent()
        {
            System.Console.WriteLine("\nLooking for most recent time event...");

            FindUserByUsername myFinder = new FindUserByUsername();
            User myUser = myFinder.Find();

            IReadAllTimeEvents allTimeEvents = new ReadTimeEvents();
            List<TimeEvent> myList = allTimeEvents.ReadAllTimeEvents();

            // myTimeEvent = myList.Find(x => x.EmployeeId == myUser.UserId && x.Date == DateTime.Now.ToString("yyyy-MM-dd"));
            myTimeEvent = myList.Find(x => x.EmployeeId == myUser.UserId);

            try
            {
                System.Console.WriteLine("Found most recent time event.\n");
                System.Console.WriteLine(myTimeEvent.ToString());
            }
            catch
            {
                System.Console.WriteLine("Could not find most recent time event.");
            }

            return myTimeEvent;
        }
    }
}