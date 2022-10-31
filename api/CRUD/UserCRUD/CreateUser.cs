using api.Interfaces;
using MySql.Data.MySqlClient;
using api.Models;
using api.database;
using Microsoft.AspNetCore.Identity;
using api.Utilities;

namespace api.CRUD
{
    public class CreateUser : ICreateOneUser
    {
        private string cs { get; }
        public CreateUser()
        {
            ConnectionString myCS = new ConnectionString();
            cs = myCS.cs;
        }
        public void CreateOneUser(User createdUser)
        {
            System.Console.WriteLine("The following user will be created...");
            System.Console.WriteLine(createdUser.ToString());

            createdUser.Password = LogInCheck.ToSHA256(createdUser.Password);

            using var con = new MySqlConnection(cs);
            con.Open();

            var stm = @"INSERT INTO employees (employeeid, firstname, lastname, username, password, ismanager) 
                VALUES (default, @firstname, @lastname, @username, @password, default);";

            using var cmd = new MySqlCommand(stm, con);

            cmd.Parameters.AddWithValue("@firstname", (createdUser.FirstName));
            cmd.Parameters.AddWithValue("@lastname", (createdUser.LastName));
            cmd.Parameters.AddWithValue("@username", (createdUser.UserName));
            cmd.Parameters.AddWithValue("@password", (createdUser.Password));
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("User has been created.");
            }
            catch (Exception e)
            {
                System.Console.WriteLine("User creation was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.Message);
            }

            //con.Close();
        }
    }
}