using api.Models;
using api.Adapters;
using api.Controllers;
using api.Interfaces;
using System.Globalization;
using api.CRUD;

namespace api.Utilities
{
    public class ApproveConflictRequest : IHandleTimeEventFromReq
    {
        public List<TimeEvent> myList = new List<TimeEvent>();
        public RequestToTimeEventAdapter myAdapter { get; set; }
        public Request myRequest { get; set; }
        public Request FindRequest(Request myRequest)
        {
            myAdapter = new RequestToTimeEventAdapter(myRequest);

            IFindTimeEventByDate myFinder = new FindTimeEventByDate();
            myList = myFinder.Find(myRequest);

            DeleteConflicts();

            ICreateOneTimeEvent myCreator = new CreateTimeEvent();
            myCreator.CreateOneTimeEvent(myAdapter);
            myRequest.Status = "created";

            return myRequest;
        }

        public void DeleteConflicts()
        {
            IDeleteOne deleteTool = new DeleteTimeEvent();
            
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