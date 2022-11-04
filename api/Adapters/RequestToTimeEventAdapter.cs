using api.Models;

namespace api.Adapters
{
    //adapter from request to time event
    public class RequestToTimeEventAdapter : TimeEvent
    {
        public RequestToTimeEventAdapter(Request myRequest)
        {
            this.Date = myRequest.Date;
            this.ClockIn = myRequest.ClockIn;
            this.ClockOut = myRequest.ClockOut;
            this.DepartmentId = myRequest.DepartmentId;
            this.Department = myRequest.Department;
            this.EmployeeId = myRequest.EmployeeId;
            this.ClockedOutCheck = "y";
        }
    }
}