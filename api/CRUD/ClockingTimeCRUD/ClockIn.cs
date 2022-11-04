using api.Interfaces;
using MySql.Data.MySqlClient;
using api.Database;
using api.Controllers;

namespace api.CRUD
{
    public class ClockIn : IClockIn
    {
        //connection string to mysql database
        private string cs { get; }
        
        public ClockIn()
        {
            ConnectionString myCS = new ConnectionString();
            cs = myCS.cs;
        }

        //create new time event with currently logged in employeeid, auto incremented event id, current date and time
        //clock out field cannot be null, so it is created with current time. a net different of 0 between clock out and clock in time would imply that the user forgot to clock out. however, this is virtually impossible
        public void ClockingIn()
        {
            System.Console.WriteLine("Clocking in...");

            using var con = new MySqlConnection(cs);
            con.Open();

            var stm = @"INSERT INTO timekeepingevents (eventid, eventdate, clockinevent, clockoutevent, eventdepartment, eventemployee, clockedoutcheck) 
                VALUES (default, @eventdate, @clockinevent, @clockoutevent, null, @eventemployee, default);";

            using var cmd = new MySqlCommand(stm, con);

            cmd.Parameters.AddWithValue("@eventemployee", LoggingIn.loggedIn.UserId);
            cmd.Parameters.AddWithValue("@eventdate", DateTime.Now.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@clockoutevent", DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("@clockinevent", DateTime.Now.ToString("HH:mm:ss"));
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("User was successfully clocked in.");
            }
            catch (Exception e)
            {
                System.Console.WriteLine("User couldn't be clocked in.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.Message);
            }
        }
    }
}