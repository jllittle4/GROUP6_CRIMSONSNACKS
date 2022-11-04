using api.Interfaces;
using api.Database;
using MySql.Data.MySqlClient;

namespace api.CRUD
{
    public class DeleteTimeEvent : IDeleteOne
    {
        //connection to mysql database
        private string cs;
        
        public DeleteTimeEvent()
        {
            ConnectionString connectionString = new ConnectionString();
            cs = connectionString.cs;
        }

        //deletes time events
        //currently used to delete time events that conflict with an approved request
        public void DeleteOne(int id)
        {
            System.Console.WriteLine($"The time event with an ID of {id} will be deleted");

            using var con = new MySqlConnection(cs);
            con.Open();

            using var cmd = new MySqlCommand();
            
            cmd.Connection = con;
            cmd.CommandText = @"DELETE FROM timekeepingevents 
                WHERE eventid = @eventid;";

            cmd.Parameters.AddWithValue("@eventid", id);
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
        }
    }
}