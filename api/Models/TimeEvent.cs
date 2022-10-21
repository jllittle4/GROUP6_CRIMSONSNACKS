namespace api.Models
{
    public class TimeEvent
    {
        //public User User = new User();

        public int TimeEventId { get; set; }
        public DateTime Date { get; set; }
        public DateTime ClockIn { get; set; }
        public DateTime ClockOut { get; set; }
        //not sure if we'll find this in the front end or the back end yet, probably back
        public int DepartmentId { get; set; }
        //same for this one
        public int EmployeeId { get; set; }


    }
}