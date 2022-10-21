using api.Interfaces;
using api.Models;
using api.database;
using MySql.Data.MySqlClient;

namespace api.CRUD
{
    public class DeleteDepartment : IDeleteOne
    {
        private string cs;
        public DeleteDepartment()
        {
            ConnectionString connectionString = new ConnectionString();
            cs = connectionString.cs;
        }
        public void DeleteOne(int id)
        {
            System.Console.WriteLine($"The driver with an ID of {id} will be deleted");

            using var con = new MySqlConnection(cs);
            con.Open();

            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @"UPDATE drivers 
                SET driverdeleted = 'y' 
                WHERE driverid = @driverid";
            cmd.Parameters.AddWithValue("@driverid", id);
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("Driver has been deleted");
            }
            catch
            {
                System.Console.WriteLine("Deletion was unsuccessful.");
            }

            // con.Close();
        }
    }
}