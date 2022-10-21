using api.Interfaces;
using api.Models;
using api.database;
using MySql.Data.MySqlClient;

namespace api.CRUD
{
    public class UpdateDepartment : IUpdateOneDepartment
    {
        private string cs;
        public UpdateDepartment()
        {
            ConnectionString connectionString = new ConnectionString();
            cs = connectionString.cs;
        }
        public void UpdateOneDepartment(int id, Department updatedDepartment)
        {
            System.Console.WriteLine($"The department with an ID of {id} will be updated to match the following details: ");
            System.Console.WriteLine(updatedDepartment.ToString());

            //double myRating = double.Parse(rating);
            using var con = new MySqlConnection(cs);
            con.Open();

            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @"UPDATE departments 
                SET departmentname = @departmentname
                WHERE departmentid = @departmentid";
            cmd.Parameters.AddWithValue("@departmentname", updatedDepartment.DepName);
            cmd.Parameters.AddWithValue("@departmentid", id);
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("Update was successful!");
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Department update was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.ToString());
            }

            // con.Close();
        }
    }
}