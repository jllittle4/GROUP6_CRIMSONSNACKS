using MySql.Data.MySqlClient;
using api.Models;
using api.Interfaces;
using api.database;

namespace api.CRUD
{
    public class ReadUsers : IReadAllUsers, IReadOneUser
    {
        private string cs { get; }
        public ReadUsers()
        {
            ConnectionString myCS = new ConnectionString();
            cs = myCS.cs;
        }
        public List<User> ReadAllUsers()
        {
            List<User> allUsers = new List<User>();

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"SELECT employeeid, firstname, lastname, username, password FROM employees;";
            // WHERE deleted = '0' ORDER BY hire_date DESC
            using var cmd = new MySqlCommand(stm, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                User temp = new User() { UserId = rdr.GetInt32(0), FirstName = rdr.GetString(1), LastName = rdr.GetString(2), UserName = rdr.GetString(3), Password = rdr.GetString(4) };
                allUsers.Add(temp);
            }

            //con.Close();

            return allUsers;
        }

        public User ReadOneUser(string searchVal)
        {
            System.Console.WriteLine("Looking for user...");

            User myUser = new User();

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"SELECT employeeid, firstname, lastname, username, password
                FROM employees 
                WHERE username = @username";

            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@username", searchVal);
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
                }
                System.Console.WriteLine("The result of the search was: ");
                System.Console.WriteLine(myUser.ToString());
            }
            catch (Exception e)
            {
                System.Console.WriteLine("User search was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.ToString());
            }

            // con.Close();
            return myUser;
        }
    }
}