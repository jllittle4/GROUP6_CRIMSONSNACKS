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
            System.Console.WriteLine("Reading all users...");

            List<User> allUsers = new List<User>();

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"SELECT employeeid, firstname, lastname, username, password, ismanager 
                FROM employees;";

            using var cmd = new MySqlCommand(stm, con);

            try
            {
                using MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    User temp = new User();

                    temp.UserId = rdr.GetInt32(0);
                    temp.FirstName = rdr.GetString(1);
                    temp.LastName = rdr.GetString(2);
                    temp.UserName = rdr.GetString(3);
                    temp.Password = rdr.GetString(4);

                    try
                    {
                        temp.IsManager = rdr.GetInt32(5);
                    }
                    catch
                    {
                        temp.IsManager = 0;
                    }

                    allUsers.Add(temp);
                }

                System.Console.WriteLine("Read all users successfully.");
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Users retrieval was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.Message);
            }



            //con.Close();

            return allUsers;
        }

        public User ReadOneUser(int id)
        {
            System.Console.WriteLine("Looking for user...");

            User myUser = new User();

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"SELECT employeeid, firstname, lastname, username, password, ismanager
                FROM employees 
                WHERE employeeid = @employeeid;";

            using var cmd = new MySqlCommand(stm, con);
            
            cmd.Parameters.AddWithValue("@employeeid", id);
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

                System.Console.WriteLine("The result of the search was: ");
                System.Console.WriteLine(myUser.ToString());
            }
            catch (Exception e)
            {
                System.Console.WriteLine("User search was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.Message);
            }

            // con.Close();
            return myUser;
        }
    }
}