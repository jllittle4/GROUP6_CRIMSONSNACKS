using api.Controllers;
using api.Interfaces;
using api.Models;
using api.CRUD;

namespace api.Utilities
{
    
    public class FindTimeEventsByEmp
    {
        public List<TimeEvent> mySingleUserList = new List<TimeEvent>();

        public List<TimeEvent> Find()
        {
            System.Console.WriteLine("\nLooking for timekeeping events by employee...");

            FindUserByUsername myFinder = new FindUserByUsername();
            User myUser = myFinder.Find();

            IReadAllTimeEvents allTimeEvents = new ReadTimeEvents();
            List<TimeEvent> myAllUsersList = allTimeEvents.ReadAllTimeEvents();

            mySingleUserList = myAllUsersList.FindAll(x => x.EmployeeId == myUser.UserId);

            try
            {
                System.Console.WriteLine("Found all timekeeping events for this employee.\n");
                System.Console.WriteLine(mySingleUserList[0].ToString());
            }
            catch
            {
                System.Console.WriteLine("Could not get list of timekeeping events.");
            }

            return mySingleUserList;
        }
    }
}