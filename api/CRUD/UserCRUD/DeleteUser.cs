using api.Interfaces;
using api.Models;
using api.database;
using MySql.Data.MySqlClient;

namespace api.CRUD
{
    public class DeleteUser : IDeleteOne
    {
        private string cs;
        public DeleteUser()
        {
            ConnectionString connectionString = new ConnectionString();
            cs = connectionString.cs;
        }
        public void DeleteOne(int id)
        {
            System.Console.WriteLine($"The user with an ID of {id} will be deleted");

            using var con = new MySqlConnection(cs);
            con.Open();

            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @"DELETE FROM employees 
                WHERE employeeid = @employeeid";
            cmd.Parameters.AddWithValue("@employeeid", id);
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("User has been deleted");
            }
            catch (Exception e)
            {
                System.Console.WriteLine("user deletion was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.ToString());
            }

            // con.Close();
        }
    }
}