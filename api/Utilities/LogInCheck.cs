using api.Interfaces;
using api.database;
using MySql.Data.MySqlClient;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using api.CRUD;
using System.Security.Cryptography;
using System.Text;

namespace api.Utilities
{
    public class LogInCheck
    {
        public bool Valid { get; set; }
        public IReadAllUsers ReaderAll { get; set; }
        public List<User> MyUsers = new List<User>();
        public LoginResult MyLoginResult { get; set; }
        public User ReturnVal { get; set; }
        public User LoginAttempt { get; set; }

        public LogInCheck(User myUser)
        {
            MyLoginResult = new LoginResult();
            this.LoginAttempt = myUser;
        }
        public LoginResult CheckValidUser(User myUser)
        {
            ReaderAll = new ReadUsers();
            MyUsers = ReaderAll.ReadAllUsers();
            ReturnVal = MyUsers.Find(x => x.UserName == myUser.UserName);

            if (ReturnVal == null)
            {
                MyLoginResult.CheckUserName = false;
                return MyLoginResult;
            }
            else
            {
                MyLoginResult.CheckUserName = true;
            }

            CheckValidPassowrd();
            return MyLoginResult;
        }

        public void CheckValidPassowrd()
        {
            string userInput = ToSHA256(LoginAttempt.Password);
            //int myResult = returnVal.Password.CompareTo(userInput);
            MyLoginResult.CheckPassword = CompareCharArrays(ReturnVal.Password, userInput);
            CheckValidAdmin();
        }

        public void CheckValidAdmin()
        {
            if (ReturnVal.IsManager == 0)
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