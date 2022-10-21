namespace api.Models
{
    public class Request
    {
        public string RequestId { get; set; }
        //public string UserName { get; set; }
        public DateTime Date { get; set; }
        public DateTime ClockIn { get; set; }
        public DateTime ClockOut { get; set; }
        public string Reason { get; set; }
        //not sure if we'll find this in the front end or the back end yet, probably back
        public int DepartmentId { get; set; }
        //same for this one
        public int EmployeeId { get; set; }

        //will check whether request is completed when querying, default is 'n'

    }
}