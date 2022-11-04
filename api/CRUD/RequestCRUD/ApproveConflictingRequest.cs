using api.Models;
using api.Adapters;
using api.Interfaces;
using api.CRUD;
using System.Globalization;

namespace api.Utilities
{
    public class ApproveConflictRequest : IHandleTimeEventFromReq
    {
        public List<TimeEvent> myList = new List<TimeEvent>();
        public RequestToTimeEventAdapter myAdapter { get; set; }
        public IFindTimeEventByDate myFinder = new FindTimeEventByDate();
        public ICreateOneTimeEvent myCreator = new CreateTimeEvent();
        public IDeleteOne deleteTool = new DeleteTimeEvent();

        //deletes time events that conflict with the time change request parameters, then creates new time event based on time change request parameters
        //returns status
        public Request FindRequest(Request myRequest)
        {
            myAdapter = new RequestToTimeEventAdapter(myRequest);

            myList = myFinder.Find(myRequest);

            DeleteConflicts();

            myCreator.CreateOneTimeEvent(myAdapter);
            myRequest.Status = "created";

            return myRequest;
        }

        //deletes time events on date of time change request that will conflict with the time change request parameters
        //a "conflict" happens when a clockin and/or clockout time on a time change request falls between a time period in a time event already in the database for that employee for that date (implying the employee was already clocked in at that time)
        public void DeleteConflicts()
        {            
            foreach (TimeEvent x in myList)
            {
                DateTime reqClockIn = DateTime.ParseExact(myAdapter.ClockIn, "hh:mm tt", new CultureInfo("en-US"));
                DateTime reqClockOut = DateTime.ParseExact(myAdapter.ClockOut, "hh:mm tt", new CultureInfo("en-US"));
                DateTime eventClockIn = DateTime.ParseExact(x.ClockIn, "hh:mm tt", new CultureInfo("en-US"));
                DateTime eventClockOut = DateTime.ParseExact(x.ClockOut, "hh:mm tt", new CultureInfo("en-US"));

                if (reqClockIn >= eventClockIn && reqClockIn < eventClockOut)
                {
                    System.Console.WriteLine(x.TimeEventId);
                    deleteTool.DeleteOne(x.TimeEventId);
                }
                else if (reqClockOut > eventClockIn && reqClockOut <= eventClockOut)
                {
                    System.Console.WriteLine(x.TimeEventId);
                    deleteTool.DeleteOne(x.TimeEventId);
                }
                else if (reqClockIn < eventClockIn && reqClockOut > eventClockOut)
                {
                    System.Console.WriteLine(x.TimeEventId);
                    deleteTool.DeleteOne(x.TimeEventId);
                }
            }
        }
    }
}