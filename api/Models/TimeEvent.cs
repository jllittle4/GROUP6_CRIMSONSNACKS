namespace api.Models
{
    public class TimeEvent
    {   
        public User User = new User();
        public DateTime Date {get; set;}
        public DateTime? ClockIn {get; set;}
        public DateTime? ClockOut {get; set;}

        
    }
}