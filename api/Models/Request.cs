namespace api.Models
{
    public class Request
    {
        public int RequestId { get; set; }
        public string Date { get; set; }
        public string ClockIn { get; set; }
        public string ClockOut { get; set; }
        public string Reason { get; set; }
        //not sure if we'll find this in the front end or the back end yet, probably back
        public int DepartmentId { get; set; }
        //same for this one
        public int EmployeeId { get; set; }
        public string Status { get; set; }
        public string TotalTime { get; set; }

        public override string ToString()
        {
            return $"{this.RequestId}\t{this.Date}\t{this.ClockIn}\t{this.ClockOut}\t{this.Reason}\t{this.DepartmentId}\t{this.EmployeeId}\t{this.Status}\t{TotalTime}";
        }

    }
}