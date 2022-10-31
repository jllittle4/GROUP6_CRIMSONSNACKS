using api.Models;

namespace api.Adapters
{
    public class DepartmentReportAdapter : Report
    {
        public DepartmentReportAdapter(DepartmentReport newReport)
        {
            this.Category = newReport.DepartmentName;
            this.Time = newReport.TotalHours.ToString();
        }
    }
}