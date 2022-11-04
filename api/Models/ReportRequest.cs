namespace api.Models
{
    //model for front end request for report type
    public class ReportRequest
    {
        public string Department { get; set; }
        public string Employee { get; set; }
        public string PayrollPeriod { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return $"Department\tEmployee\tStart Date\tEnd Date\tPeriod\tType\n{this.Department}\t{this.Employee}\t{this.StartDate}\t{this.EndDate}\t{this.PayrollPeriod}\t{this.Type}";
        }
    }
}