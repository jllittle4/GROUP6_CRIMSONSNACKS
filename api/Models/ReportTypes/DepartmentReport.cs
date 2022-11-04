namespace api.Models
{
    //model for all total time by department by month reports
    //potential to use this object for total time by department by day, by week, by year, etc.
    public class DepartmentReport
    {
        public string DepartmentName { get; set; }
        public double TotalHours { get; set; }
    }
}