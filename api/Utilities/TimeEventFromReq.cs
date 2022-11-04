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
        public Request returnRequest = new Request();
        public Request FindRequest(Request myRequest)
        {
            myAdapter = new RequestToTimeEventAdapter(myRequest);
            returnRequest = myRequest;

            IFindTimeEventByDate myFinder = new FindTimeEventByDate();
            myList = myFinder.Find(myRequest);

            if (myList.Count == 0)
            {
                System.Console.WriteLine("No time events were found.");
                ICreateOneTimeEvent myCreator = new CreateTimeEvent();
                myCreator.CreateOneTimeEvent(myAdapter);
                returnRequest.Status = "created";
            }
            else
            {
                System.Console.WriteLine("Searching through returned time events...");

                bool noConflicts = CheckNoConflicts();

                if (noConflicts)
                {
                    ICreateOneTimeEvent myCreator = new CreateTimeEvent();
                    myCreator.CreateOneTimeEvent(myAdapter);
                    returnRequest.Status = "created";
                }
                else
                {
                    int isOneEqual = IsOneEqual();

                    if (isOneEqual != -1)
                    {
                        IUpdateOneTimeEvent myUpdater = new UpdateTimeEvent();
                        myUpdater.UpdateOneTimeEvent(isOneEqual, myAdapter);
                        returnRequest.Status = "updated";
                    }
                    else
                    {
                        returnRequest.Status = "warning";
                        //in the front end, prompt for a confirm or deny 
                        //if confirm, delete all requests which have a conflict in the check no conflicts method, and then create a new time event
                        //if deny, deny request
                    }
                }
            }
            System.Console.WriteLine("Returning the request...");
            return returnRequest;
        }

        public bool CheckNoConflicts()
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

        public int IsOneEqual()
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