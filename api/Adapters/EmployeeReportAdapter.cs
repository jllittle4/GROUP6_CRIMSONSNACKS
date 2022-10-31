using api.Models;

namespace api.Adapters
{
    public class EmployeeReportAdapter : Report
    {
        public EmployeeReportAdapter(EmployeeReport newReport)
        {
            this.Category = newReport.FullName;
            this.Time = newReport.TotalHours.ToString();
        }
    }
}