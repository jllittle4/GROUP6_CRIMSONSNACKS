using api.Interfaces;
using api.Models;
using api.database;
using MySql.Data.MySqlClient;

namespace api.CRUD
{
    public class UpdateTimeEvent : IUpdateOneTimeEvent
    {
        private string cs;
        public UpdateTimeEvent()
        {
            ConnectionString connectionString = new ConnectionString();
            cs = connectionString.cs;
        }
        public void UpdateOneTimeEvent(int id, TimeEvent updatedUser)
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