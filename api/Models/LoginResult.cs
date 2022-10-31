namespace api.Models
{
    public class LoginResult
    {
        public bool IsAdmin { get; set; }
        public bool CheckUserName { get; set; }
        public bool CheckPassword { get; set; }

        public LoginResult()
        {
            this.IsAdmin = false;
            this.CheckPassword = false;
            this.CheckUserName = false;
        }

        public override string ToString()
        {
            return $"Admin?\tUsername?\tPassword?\n{this.IsAdmin}\t{this.CheckUserName}\t\t{this.CheckPassword}";
        }
    }
}