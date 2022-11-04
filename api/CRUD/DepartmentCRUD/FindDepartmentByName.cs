using api.Interfaces;
using api.Models;
using api.Database;
using MySql.Data.MySqlClient;

namespace api.Utilities
{
    public class FindDepartmentByName : IReadDepByName
    {
        //connection to mysql database
        private string cs { get; }
        public Department myDep = new Department();

        public FindDepartmentByName()
        {
            ConnectionString connectionString = new ConnectionString();
            this.cs = connectionString.cs;
        }

        //find department by department name, since all department names are unique
        public Department Find(string depname)
        {
            System.Console.WriteLine("Looking for department id...\n");

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"SELECT *
                FROM departments
                WHERE departmentname = @departmentname;";

            using var cmd = new MySqlCommand(stm, con);

            cmd.Parameters.AddWithValue("@departmentname", depname);
            cmd.Prepare();

            try
            {
                using MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    myDep.DepId = rdr.GetInt32(0);
                    myDep.DepName = rdr.GetString(1);
                }

                System.Console.WriteLine("The result of the search was: \n");
                System.Console.WriteLine(myDep.ToString());
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Department search was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.ToString());
            }

            return myDep;
        }
    }
}