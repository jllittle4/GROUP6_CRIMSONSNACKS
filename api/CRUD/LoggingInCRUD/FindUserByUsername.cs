using api.Interfaces;
using api.Models;
using api.Database;
using MySql.Data.MySqlClient;

namespace api.Utilities
{
    public class FindUserByUsername : IFindUserByUsername
    {
        //connection string to mysql database
        private string cs { get; }
        public User myUser = new User();
        
        public FindUserByUsername()
        {
            ConnectionString connectionString = new ConnectionString();
            this.cs = connectionString.cs;
        }

        //finds a user by username, since all usernames have to be unique
        public User Find(string username)
        {
            System.Console.WriteLine("Looking for an employee...");

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"SELECT employeeid, firstname, lastname, username, password, ismanager
                FROM employees 
                WHERE username = @username;";

            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Prepare();

            try
            {
                using MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    myUser.UserId = rdr.GetInt32(0);
                    myUser.FirstName = rdr.GetString(1);
                    myUser.LastName = rdr.GetString(2);
                    myUser.UserName = rdr.GetString(3);
                    myUser.Password = rdr.GetString(4);

                    try
                    {
                        myUser.IsManager = rdr.GetInt32(5);
                    }
                    catch
                    {
                        myUser.IsManager = 0;
                    }
                }

                System.Console.WriteLine("Found employee.");
                System.Console.WriteLine(myUser.ToString());
            }
            catch
            {
                System.Console.WriteLine("Couldn't find employee.");
            }

            return myUser;
        }
    }
}