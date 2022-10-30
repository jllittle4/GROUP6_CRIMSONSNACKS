using api.Controllers;
using api.Interfaces;
using api.Models;
using api.CRUD;

namespace api.Utilities
{
    
    public class FindUserByUsername
    {
        public User myUser { get; set; }

        public User Find()
        {
            System.Console.WriteLine("\nLooking for logged in user...");
            IReadAllUsers allUsers = new ReadUsers();
            List<User> myList = allUsers.ReadAllUsers();

            myUser = myList.Find(x => x.UserName == Users.loggedIn.UserName);

            try
            {
                System.Console.WriteLine("Found logged in user.\n");
                System.Console.WriteLine(myUser.ToString());
            }
            catch
            {
                System.Console.WriteLine("Could not find user.");
            }

            return myUser;
        }
    }
}