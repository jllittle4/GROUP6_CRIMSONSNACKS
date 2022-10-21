using api.Interfaces;
using api.Models;
using api.database;
using MySql.Data.MySqlClient;

namespace api.CRUD
{
    public class UpdateRequest : IUpdateOneRequest
    {
        private string cs;
        public UpdateRequest()
        {
            ConnectionString connectionString = new ConnectionString();
            cs = connectionString.cs;
        }
        public void UpdateOneRequest(int id, Request updatedUser)
        {
            System.Console.WriteLine($"The driver with an ID of {id} will be updated to match the following details: ");
            System.Console.WriteLine(updatedUser.ToString());

            //double myRating = double.Parse(rating);
            using var con = new MySqlConnection(cs);
            con.Open();

            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @"UPDATE drivers 
                SET driverrating = @driverrating 
                WHERE driverid = @driverid";
            cmd.Parameters.AddWithValue("@driverrating", updatedUser);
            cmd.Parameters.AddWithValue("@driverid", id);
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("Update was successful!");
            }
            catch
            {
                System.Console.WriteLine("Update was unsuccessful.");
            }

            // con.Close();
        }
    }
}