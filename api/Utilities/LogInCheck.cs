using api.Interfaces;
using api.Models;
using api.CRUD;
using System.Security.Cryptography;
using System.Text;

namespace api.Utilities
{
    public class LogInCheck
    {
        public LoginResult MyLoginResult { get; set; }
        public User LoginAttempt { get; set; }

        public LogInCheck(User myUser)
        {
            MyLoginResult = new LoginResult();
            this.LoginAttempt = myUser;
        }

        public LoginResult CheckValidPassword(string userInput)
        {
            MyLoginResult.CheckUserName = true;

            userInput = ToSHA256(userInput);
            MyLoginResult.CheckPassword = CompareCharArrays(LoginAttempt.Password, userInput);

            CheckValidAdmin();
            return MyLoginResult;
        }

        public void CheckValidAdmin()
        {
            if (LoginAttempt.IsManager == 0)
            {
                MyLoginResult.IsAdmin = false;
            }
            else
            {
                MyLoginResult.IsAdmin = true;
            }
        }

        public bool CompareCharArrays(string s, string t)
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