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

            //ConnectionString myConnection = new ConnectionString();
            //string cs = myConnection.cs;

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

        public User ReadOneUser(int id)
        {
            System.Console.WriteLine("Looking for driver...");

            User myDriver = new User();

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"SELECT driverid, drivername, driverrating, 
                DATE_FORMAT(driverhiredate, '%M %e, %Y') 
                    AS format_driverhiredate 
                FROM drivers 
                WHERE driverid = @id";

            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            try
            {
                using MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // myDriver.ID = rdr.GetInt32(0);
                    // myDriver.Name = rdr.GetString(1);
                    // myDriver.Rating = rdr.GetDouble(2);
                    // myDriver.Date = rdr.GetString(3);
                    // myDriver.Deleted = "n";
                }
                System.Console.WriteLine("The result of the search was: ");
                System.Console.WriteLine(myDriver.ToString());
            }
            catch
            {
                System.Console.WriteLine("The search was unsuccessful.");
            }

            // con.Close();
            return myDriver;
        }
    }
}