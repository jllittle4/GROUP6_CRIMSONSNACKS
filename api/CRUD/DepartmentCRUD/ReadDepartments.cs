using api.Models;
using api.Interfaces;
using api.Database;
using MySql.Data.MySqlClient;

namespace api.CRUD
{
    public class ReadDepartments : IReadAllDepartments, IReadOneDepartment
    {
        //connection string to mysql database
        private string cs { get; }
        public List<Department> allDepartments = new List<Department>();
        public Department myDepartment = new Department();
        
        public ReadDepartments()
        {
            ConnectionString myCS = new ConnectionString();
            cs = myCS.cs;
        }

        //returns list of all departments to populate dropdowns
        public List<Department> ReadAllDepartments()
        {
            System.Console.WriteLine("Reading all departments...");

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"SELECT * 
                FROM departments 
                ORDER BY departmentid ASC;";

            using var cmd = new MySqlCommand(stm, con);

            try
            {
                using MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Department temp = new Department()
                    {
                        DepId = rdr.GetInt32(0),
                        DepName = rdr.GetString(1)
                    };
                    allDepartments.Add(temp);
                }

                System.Console.WriteLine("Read all departments successfully.");
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Departments retrieval was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.Message);
            }

            return allDepartments;
        }

        //returns one department
        //currently not used
        public Department ReadOneDepartment(int id)
        {
            System.Console.WriteLine("Looking for department...");

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"SELECT * 
                FROM departments
                WHERE departmentid = @departmentid;";

            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@departmentid", id);
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
                System.Console.WriteLine(e.Message);
            }

            return myDepartment;
        }
    }
}