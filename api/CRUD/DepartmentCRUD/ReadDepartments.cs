using MySql.Data.MySqlClient;
using api.Models;
using api.Interfaces;
using api.database;

namespace api.CRUD
{
    public class ReadDepartments : IReadAllDepartments, IReadOneDepartment
    {
        private string cs { get; }
        public ReadDepartments()
        {
            ConnectionString myCS = new ConnectionString();
            cs = myCS.cs;
        }
        public List<Department> ReadAllDepartments()
        {
            List<Department> allDepartments = new List<Department>();

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"SELECT * FROM departments;";
            using var cmd = new MySqlCommand(stm, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Department temp = new Department(){DepId = rdr.GetInt32(0), DepName = rdr.GetString(1)};
                allDepartments.Add(temp);
            }

            //con.Close();

            return allDepartments;
        }

        public Department ReadOneDepartment(string departmentName)
        {
            System.Console.WriteLine("Looking for department...");

            Department myDepartment = new Department();

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"SELECT * 
                FROM departments
                WHERE departmentname LIKE @departmentname";

            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@departmentname", departmentName);
            cmd.Prepare();

            try
            {
                using MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    myDepartment.DepId = rdr.GetInt32(0);
                    myDepartment.DepName = rdr.GetString(1);
                }
                System.Console.WriteLine("The result of the search was: ");
                System.Console.WriteLine(myDepartment.ToString());
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Department search was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.ToString());
            }

            // con.Close();
            return myDepartment;
        }
    }
}