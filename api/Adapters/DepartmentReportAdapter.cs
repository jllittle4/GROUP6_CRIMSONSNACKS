using api.Models;

namespace api.Adapters
{
    //adapter for department report object to generic report object
    public class DepartmentReportAdapter : Report
    {
        public DepartmentReportAdapter(DepartmentReport newReport)
        {
            this.Category = newReport.DepartmentName;
            this.Time = newReport.TotalHours.ToString();
        }
    }
}