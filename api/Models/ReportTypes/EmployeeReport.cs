namespace api.Models
{
    //model for all total time by employee by month reports
    //potential to use this object for total time by employee by day, by week, by year, etc.
    public class EmployeeReport
    {
        public string FullName { get; set; }
        public double TotalHours { get; set; }
    }
}