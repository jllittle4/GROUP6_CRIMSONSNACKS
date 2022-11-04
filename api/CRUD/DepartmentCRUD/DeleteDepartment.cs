using api.Interfaces;
using api.Models;
using api.Database;
using MySql.Data.MySqlClient;

namespace api.CRUD
{
    public class DeleteDepartment : IDeleteOne
    {
        //connection string to mysql database
        private string cs;
        
        public DeleteDepartment()
        {
            ConnectionString connectionString = new ConnectionString();
            cs = connectionString.cs;
        }

        //delete a department
        //currently not used, but potential for admin to remove a department if organization is restructured, all parts of application will automatically update list of departments
        public void DeleteOne(int id)
        {
            System.Console.WriteLine($"The department with an ID of {id} will be deleted");

            using var con = new MySqlConnection(cs);
            con.Open();

            using var cmd = new MySqlCommand();
            
            cmd.Connection = con;
            cmd.CommandText = @"DELETE FROM departments 
                WHERE departmentid = @departmentid;";

            cmd.Parameters.AddWithValue("@departmentid", id);
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("Department has been deleted");
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Department deletion was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.Message);
            }

            // con.Close();
        }
    }
}