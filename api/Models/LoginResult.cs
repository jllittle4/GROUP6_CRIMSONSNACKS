namespace api.Models
{
    //model for verifying user login attempt
    public class LoginResult
    {
        public bool IsAdmin { get; set; }
        public bool CheckUserName { get; set; }
        public bool CheckPassword { get; set; }

        //sets default login result to all be false for security
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