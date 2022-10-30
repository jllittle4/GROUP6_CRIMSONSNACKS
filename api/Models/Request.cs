    namespace api.Models
{
    public class Request
    {
        public int RequestId { get; set; }
        public string Date { get; set; }
        public string ClockIn { get; set; }
        public string ClockOut { get; set; }
        public string Reason { get; set; }
        public int DepartmentId { get; set; }
        public int EmployeeId { get; set; }
        public string Status { get; set; }
        public string TotalTime { get; set; }

        public override string ToString()
        {
            return "ID\tDate\tClockIn\tClockOut\tReason\tDepID\tEmpID\tStatus\tNet Hours" +
                $"{this.RequestId}\t{this.Date}\t{this.ClockIn}\t{this.ClockOut}\t{this.Reason.Substring(0, 6)}\t{this.DepartmentId}\t{this.EmployeeId}\t{this.Status}\t{TotalTime}";
        }

    }
}