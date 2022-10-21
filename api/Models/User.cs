namespace api.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        //will check ismanager when doing admin login check, default 'n'

        public override string ToString()
        {
            return $"{this.UserId}\t{this.FirstName}\t{this.LastName}\t{this.UserName}";
        }
    }
}