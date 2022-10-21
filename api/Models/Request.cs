namespace api.Models
{
    public class Request
    {
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly Clock { get; set; }

    }
}