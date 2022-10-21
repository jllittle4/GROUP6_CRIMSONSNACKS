using api.Interfaces;
using api.database;
using MySql.Data.MySqlClient;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using api.CRUD;


namespace api.Utilities
{
    public class LogInCheck
    {
        public bool valid { get; set; }
        public IReadAllUsers readerAll { get; set; }
        public List<User> users = new List<User>();
        public User returnVal { get; set; }
        //public User temp = new User();

        public void Check(User loginAttempt)
        { //searches for the driver the user inputs
            readerAll = new ReadUsers();
            users = readerAll.ReadAllUsers();
            returnVal = users.Find(x => x.UserName == loginAttempt.UserName);

            if (returnVal is null)
            {
                System.Console.WriteLine("User not found.");
                valid = false;
            }
            else
            {
                //CheckPassword();
                IPasswordHasher<User> myPasswordHasher = new PasswordHasher<User>();
                loginAttempt.Password = myPasswordHasher.HashPassword(loginAttempt, loginAttempt.Password);
                PasswordVerificationResult myResult = myPasswordHasher.VerifyHashedPassword(returnVal, returnVal.Password, loginAttempt.Password);

                if (((int)myResult) == 1)
                {
                    System.Console.WriteLine("Correct password");
                    valid = true;
                }
                else
                {
                    System.Console.WriteLine("Incorrect password");
                    valid = false;
                }

            }

            // if (returnVal.UserName == temp.UserName)
            // {
            //     CheckPassword();
            // }
            // else
            // {
            //     System.Console.WriteLine("Username not found, please try again");

            //     valid = false;

        }

        // public void CheckPassword()
        // {
        //     readerAll = new ReadUsers();
        //     users = readerAll.ReadAllUsers();
        //     //IPasswordHasher<User> myPasswordHasher = new PasswordHasher<User>();
        //     returnVal = users.Find(x => x.Password == temp.Password);
        //     //myPasswordHasher.VerifyHashedPassword(returnVal, temp.Password);


        //     if (returnVal.Password == temp.Password)
        //     {
        //         System.Console.WriteLine("Login Successful");
        //         valid = true;
        //     }
        //     else
        //     {
        //         System.Console.WriteLine("Username not found, please try again");
        //         valid = false;
        //     }
        // }

    }

}
