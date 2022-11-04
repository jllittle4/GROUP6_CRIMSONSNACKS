using api.Models;

namespace api.Adapters
{
    //adapter for employee report object to generic report object
    public class EmployeeReportAdapter : Report
    {
        public EmployeeReportAdapter(EmployeeReport newReport)
        {
            this.Category = newReport.FullName;
            this.Time = newReport.TotalHours.ToString();
        }
    }
}