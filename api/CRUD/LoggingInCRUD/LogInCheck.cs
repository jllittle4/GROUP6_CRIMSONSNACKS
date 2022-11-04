using api.Models;
using System.Security.Cryptography;
using System.Text;

namespace api.Utilities
{
    public class LogInCheck
    {
        public LoginResult myLoginResult { get; set; }
        public User loginAttempt { get; set; }

        public LogInCheck(User myUser)
        {
            myLoginResult = new LoginResult();
            this.loginAttempt = myUser;
        }

        //returns loginresult object to check whether user that attempted to login had a valid username, valid password, and whether they were or weren't an admin
        public LoginResult CheckValidPassword(string userInput)
        {
            myLoginResult.CheckUserName = true;

            userInput = ToSHA256(userInput);
            myLoginResult.CheckPassword = CompareCharArrays(loginAttempt.Password, userInput);

            CheckValidAdmin();
            return myLoginResult;
        }

        //checks whether the user was or wasn't an admin
        //this field is determined by "ismanager" column in database
        private void CheckValidAdmin()
        {
            if (loginAttempt.IsManager == 0)
            {
                myLoginResult.IsAdmin = false;
            }
            else
            {
                myLoginResult.IsAdmin = true;
            }
        }

        //compares length of hashes and whether are an exact match
        private bool CompareCharArrays(string s, string t)
        {
            if (s.Length != t.Length)
            {
                return false;
            }

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != t[i])
                {
                    return false;
                }
            }

            return true;
        }

        //hashes a password for new employees and login attempts
        public static string ToSHA256(string s)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));

            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}