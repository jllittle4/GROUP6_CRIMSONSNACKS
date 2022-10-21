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
        public void UpdateOneTimeEvent(int id, TimeEvent updatedTimeEvent)
        {
            System.Console.WriteLine($"The time event with an ID of {id} will be updated to match the following details: ");
            System.Console.WriteLine(updatedTimeEvent.ToString());

            //double myRating = double.Parse(rating);
            using var con = new MySqlConnection(cs);
            con.Open();

            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @"UPDATE timekeepingevents
                SET clockedoutcheck = 'y''
                WHERE eventid = @eventid";
            cmd.Parameters.AddWithValue("@eventid", id);
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("Time event update was successful!");
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Time event update was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.ToString());
            }

            // con.Close();
        }
    }
}