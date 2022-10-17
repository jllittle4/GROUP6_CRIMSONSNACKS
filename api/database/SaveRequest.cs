using api.Interfaces;

using MySql.Data.MySqlClient;
using api.Models;

namespace api.database
{
    public class SaveRequest : ISeedRequest
    {
        public User? temp {get; set;}
        public void SeedRequest()
        {
            ConnectionString myConnection = new ConnectionString();
            // Driver temp = new Driver();
            

            string cs = myConnection.cs;
            using var con = new MySqlConnection(cs);

            con.Open();
            var stm = "INSERT INTO EMPLOYEES (FName, LName, UserName, Password) values (@FName, @LName, @UserName, @Password);";
            using (var cmd = new MySqlCommand(stm, con)){

                //cmd.CommandText = "INSERT INTO drivers (name, hire_date, rating, deleted) values (@EmpName, @HireDate, @rating, @Deleted);";
            
                cmd.Parameters.AddWithValue("@FName", (temp.FirstName));
                cmd.Parameters.AddWithValue("@LName", (temp.LastName));
                cmd.Parameters.AddWithValue("@UserName", (temp.UserName));
                cmd.Parameters.AddWithValue("@Password", (temp.Password));
                cmd.Prepare();
            

                cmd.ExecuteNonQuery();
            }

            con.Close();
            

        }
    }
}