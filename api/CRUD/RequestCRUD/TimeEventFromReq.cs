using api.Models;
using api.Interfaces;
using api.CRUD;
using api.Adapters;
using System.Globalization;

namespace api.Utilities
{
    public class TimeEventFromReq : IHandleTimeEventFromReq
    {
        public List<TimeEvent> myList = new List<TimeEvent>();
        public RequestToTimeEventAdapter myAdapter { get; set; }
        public IFindTimeEventByDate myFinder = new FindTimeEventByDate();
        public ICreateOneTimeEvent myCreator = new CreateTimeEvent();
        public IUpdateOneTimeEvent myUpdater = new UpdateTimeEvent();
        public Request returnRequest = new Request();
        public bool noConflicts { get; set; }
        public int isOneEqual { get; set; }

        //if time change request is approved, checks that time change request won't conflict with other time events for that date for that employee
        //if it doesn't, it creates time event or updates time event
        //returns status
        public Request FindRequest(Request myRequest)
        {
            myAdapter = new RequestToTimeEventAdapter(myRequest);
            returnRequest = myRequest;

            myList = myFinder.Find(myRequest);

            if (myList.Count == 0)
            {
                System.Console.WriteLine("No time events were found.");
                myCreator.CreateOneTimeEvent(myAdapter);
                returnRequest.Status = "created";
            }
            else
            {
                System.Console.WriteLine("Searching through returned time events...");

                noConflicts = CheckNoConflicts();

                if (noConflicts)
                {
                    myCreator.CreateOneTimeEvent(myAdapter);
                    returnRequest.Status = "created";
                }
                else
                {
                    isOneEqual = IsOneEqual();

                    if (isOneEqual != -1)
                    {
                        myUpdater.UpdateOneTimeEvent(isOneEqual, myAdapter);
                        returnRequest.Status = "updated";
                    }
                    else
                    {
                        returnRequest.Status = "warning";
                    }
                }
            }

            System.Console.WriteLine("Returning the request...");
            return returnRequest;
        }

        //checks that time change request parameters don't fall between times that the employee is already as marked as clocked in on date of the time change request
        private bool CheckNoConflicts()
        {
            foreach (TimeEvent x in myList)
            {
                DateTime reqClockIn = DateTime.ParseExact(myAdapter.ClockIn, "hh:mm tt", new CultureInfo("en-US"));
                DateTime reqClockOut = DateTime.ParseExact(myAdapter.ClockOut, "hh:mm tt", new CultureInfo("en-US"));
                DateTime eventClockIn = DateTime.ParseExact(x.ClockIn, "hh:mm tt", new CultureInfo("en-US"));
                DateTime eventClockOut = DateTime.ParseExact(x.ClockOut, "hh:mm tt", new CultureInfo("en-US"));

                if (reqClockIn >= eventClockIn && reqClockIn < eventClockOut)
                {
                    return false;
                }
                else if (reqClockOut > eventClockIn && reqClockOut <= eventClockOut)
                {
                    return false;
                }
                else if (reqClockIn < eventClockIn && reqClockOut > eventClockOut)
                {
                    return false;
                }
            }

            System.Console.WriteLine("No time conflicts found.");
            return true;
        }

        //determines whether to create new event or update old event
        //if a clock in or clock out is the same as a time event already on that date, a time event will be updated
        //otherwise, a time event will be created
        private int IsOneEqual()
        {
            foreach (TimeEvent x in myList)
            {
                DateTime reqClockIn = DateTime.ParseExact(myAdapter.ClockIn, "hh:mm tt", new CultureInfo("en-US"));
                DateTime reqClockOut = DateTime.ParseExact(myAdapter.ClockOut, "hh:mm tt", new CultureInfo("en-US"));
                DateTime eventClockIn = DateTime.ParseExact(x.ClockIn, "hh:mm tt", new CultureInfo("en-US"));
                DateTime eventClockOut = DateTime.ParseExact(x.ClockOut, "hh:mm tt", new CultureInfo("en-US"));

                if (reqClockIn == eventClockIn || reqClockOut == eventClockOut)
                {
                    return x.TimeEventId;
                }
            }

            return -1;
        }
    }
}