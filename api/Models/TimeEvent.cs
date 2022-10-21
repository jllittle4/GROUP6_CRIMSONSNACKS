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
        public double TotalTime { get; set; }

        public override string ToString()
        {
            return $"{this.TimeEventId}\t{this.Date}\t{this.ClockIn}\t{this.ClockOut}\t{this.DepartmentId}\t{this.EmployeeId}";
        }
    }
}