namespace api.Models
{
    //model for "timekeepingevents" table from database
    public class TimeEvent
    {
        public int TimeEventId { get; set; }
        public string Date { get; set; }
        public string ClockIn { get; set; }
        public string ClockOut { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public int EmployeeId { get; set; }
        public string TotalTime { get; set; }
        public string ClockedOutCheck { get; set; }

        public override string ToString()
        {
            return "ID\tDate\t\t\tClockIn\t\tClockOut\tDepartment\tEmpID\tNet Time\tClocked Out?\n" + 
                $"{this.TimeEventId}\t{this.Date}\t{this.ClockIn}\t{this.ClockOut}\t{this.Department}\t{this.EmployeeId}\t{this.TotalTime}\t\t{this.ClockedOutCheck}";
        }
    }
}