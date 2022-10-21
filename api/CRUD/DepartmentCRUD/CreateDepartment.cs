using api.Interfaces;
using MySql.Data.MySqlClient;
using api.Models;
using api.database;

namespace api.CRUD
{
    public class CreateDepartment : ICreateOneDepartment
    {
        //public User? temp {get; set;}
        private string cs { get; }
        public CreateDepartment()
        {
            ConnectionString myCS = new ConnectionString();
            cs = myCS.cs;
        }
        public void CreateOneDepartment(Department temp)
        {
            //ConnectionString myConnection = new ConnectionString();
            // Driver temp = new Driver();
            
            //string cs = myConnection.cs;

            using var con = new MySqlConnection(cs);

            con.Open();
            var stm = "INSERT INTO EMPLOYEES (departmentid, departmentname) values (default, @departmentname);";
            using (var cmd = new MySqlCommand(stm, con)){

                //cmd.CommandText = "INSERT INTO drivers (name, hire_date, rating, deleted) values (@EmpName, @HireDate, @rating, @Deleted);";
            
                cmd.Parameters.AddWithValue("@departmentname", (temp.DepName));
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }

            //con.Close();
        }
    }
}