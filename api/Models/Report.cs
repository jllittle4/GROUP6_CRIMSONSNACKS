namespace api.Models
{
    //parent model for reports
    //wrappers/adapters for other report objects have been written inheriting from this model
    public class Report
    {
        public string Category { get; set; }
        public string Time { get; set; }
    }
}