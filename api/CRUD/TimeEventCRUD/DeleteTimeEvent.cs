using api.Interfaces;
using api.Models;
using api.database;
using MySql.Data.MySqlClient;

namespace api.CRUD
{
    public class DeleteTimeEvent : IDeleteOne
    {
        private string cs;
        public DeleteTimeEvent()
        {
            ConnectionString connectionString = new ConnectionString();
            cs = connectionString.cs;
        }
        public void DeleteOne(int id)
        {
            System.Console.WriteLine($"The time event with an ID of {id} will be deleted");

            using var con = new MySqlConnection(cs);
            con.Open();

            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @"DELETE FROM timekeepingevents 
                WHERE eventid = @eventid";
            cmd.Parameters.AddWithValue("@driverid", id);
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("The time event has been deleted.");
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Event deletion was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.ToString());
            }

            // con.Close();
        }
    }
}