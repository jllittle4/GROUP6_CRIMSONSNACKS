using api.Interfaces;
using MySql.Data.MySqlClient;
using api.Models;
using api.database;

namespace api.CRUD
{
    public class CreateDepartment : ICreateOneDepartment
    {
        private string cs { get; }
        public CreateDepartment()
        {
            ConnectionString myCS = new ConnectionString();
            cs = myCS.cs;
        }
        public void CreateOneDepartment(Department newDepartment)
        {
            System.Console.WriteLine("The following department will be created...");
            System.Console.WriteLine(newDepartment.ToString());

            using var con = new MySqlConnection(cs);

            con.Open();
            var stm = "INSERT INTO EMPLOYEES (departmentid, departmentname) VALUES (default, @departmentname);";
            using var cmd = new MySqlCommand(stm, con);

            cmd.Parameters.AddWithValue("@departmentname", (newDepartment.DepName));
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("The department has been created.");
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Department creation was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.ToString());
            }
        }
    }
}