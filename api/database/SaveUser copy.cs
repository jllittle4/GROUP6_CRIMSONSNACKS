using api.Interfaces;

using MySql.Data.MySqlClient;
using api.Models;

namespace api.database
{
    public class SaveUser : ISeedUser
    {
        public User? temp {get; set;}
        public void SeedUser()
        {
            ConnectionString myConnection = new ConnectionString();
            // Driver temp = new Driver();
            

            string cs = myConnection.cs;
            using var con = new MySqlConnection(cs);

            con.Open();
            var stm = "INSERT INTO EMPLOYEES (firstname, lastname, username, password) values (@firstname, @lastname, @username, @password);";
            using (var cmd = new MySqlCommand(stm, con)){

                //cmd.CommandText = "INSERT INTO drivers (name, hire_date, rating, deleted) values (@EmpName, @HireDate, @rating, @Deleted);";
            
                cmd.Parameters.AddWithValue("@firstname", (temp.FirstName));
                cmd.Parameters.AddWithValue("@lastname", (temp.LastName));
                cmd.Parameters.AddWithValue("@username", (temp.UserName));
                cmd.Parameters.AddWithValue("@password", (temp.Password));
                cmd.Prepare();
            

                cmd.ExecuteNonQuery();
            }

            con.Close();
            

        }
    }
}