namespace api.Models
{
    public class TimeEvent
    {
        public int TimeEventId { get; set; }
        public string Date { get; set; }
        public string ClockIn { get; set; }
        public string ClockOut { get; set; }
        public int? DepartmentId { get; set; }
        public int EmployeeId { get; set; }
        public string TotalTime { get; set; }

        public override string ToString()
        {
            return "ID\tDate\t\tClockIn\t\tClockOut\tDepID\tEmpID\tNet Time\n" + 
                $"{this.TimeEventId}\t{this.Date}\t{this.ClockIn}\t{this.ClockOut}\t{this.DepartmentId}\t{this.EmployeeId}";
        }
    }
}