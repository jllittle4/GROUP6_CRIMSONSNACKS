namespace api.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int IsManager { get; set; }

        public override string ToString()
        {
            return "ID\tFirst Name\tLast Name\tUser Name\tAdmin?" + 
                $"{this.UserId}\t{this.FirstName}\t{this.LastName}\t{this.UserName}\t{this.IsManager}";
        }
    }
}