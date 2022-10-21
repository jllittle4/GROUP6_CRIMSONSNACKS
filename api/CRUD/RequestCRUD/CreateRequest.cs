using api.Interfaces;
using api.database;
using MySql.Data.MySqlClient;
using api.Models;

namespace api.CRUD
{
    public class CreateRequest : ICreateOneRequest
    {
        //not done
        //public User? temp {get; set;}
        private string cs { get; }
        public CreateRequest()
        {
            ConnectionString myCS = new ConnectionString();
            cs = myCS.cs;
        }

        public void CreateOneRequest(Request temp)
        {
            //ConnectionString myConnection = new ConnectionString();
            // Driver temp = new Driver();

            //string cs = myConnection.cs;

            using var con = new MySqlConnection(cs);
            con.Open();

            var stm = "INSERT INTO requests (firstname, lastname, username, password) VALUES (@firstname, @lastname, @username, @password);";
            using (var cmd = new MySqlCommand(stm, con))
            {

                //cmd.CommandText = "INSERT INTO drivers (name, hire_date, rating, deleted) values (@EmpName, @HireDate, @rating, @Deleted);";

                // cmd.Parameters.AddWithValue("@firstname", (temp.FirstName));
                // cmd.Parameters.AddWithValue("@lastname", (temp.LastName));
                // cmd.Parameters.AddWithValue("@username", (temp.UserName));
                // cmd.Parameters.AddWithValue("@password", (temp.Password));
                cmd.Prepare();


                cmd.ExecuteNonQuery();
            }

            //con.Close();
        }
    }
}