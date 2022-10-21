using api.Interfaces;
using MySql.Data.MySqlClient;
using api.Models;
using api.database;
using Microsoft.AspNetCore.Identity;

namespace api.CRUD
{
    public class CreateUser : ICreateOneUser
    {
        //public User? temp {get; set;}
        private string cs { get; }
        public CreateUser()
        {
            ConnectionString myCS = new ConnectionString();
            cs = myCS.cs;
        }
        public void CreateOneUser(User createdUser)
        {
            //ConnectionString myConnection = new ConnectionString();
            // Driver temp = new Driver();

            //string cs = myConnection.cs;

            IPasswordHasher<User> myHasher = new PasswordHasher<User>();
            createdUser.Password = myHasher.HashPassword(createdUser, createdUser.Password);

            using var con = new MySqlConnection(cs);

            con.Open();
            var stm = "INSERT INTO EMPLOYEES (firstname, lastname, username, password) values (@firstname, @lastname, @username, @password);";
            using (var cmd = new MySqlCommand(stm, con))
            {
                cmd.Parameters.AddWithValue("@firstname", (createdUser.FirstName));
                cmd.Parameters.AddWithValue("@lastname", (createdUser.LastName));
                cmd.Parameters.AddWithValue("@username", (createdUser.UserName));
                cmd.Parameters.AddWithValue("@password", (createdUser.Password));
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }

            //con.Close();
        }
    }
}