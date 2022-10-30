using api.Interfaces;
using api.Models;
using api.database;
using MySql.Data.MySqlClient;
using api.Utilities;

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

            using var con = new MySqlConnection(cs);
            con.Open();

            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @"UPDATE timekeepingevents
                SET clockedoutcheck = 'y', 
                    clockoutevent = @clockoutevent, 
                    eventdepartment = @eventdepartment, 
                    eventdate = @eventdate, 
                    clockinevent = @clockinevent
                WHERE eventid = @eventid;";

            cmd.Parameters.AddWithValue("@eventid", id);
            cmd.Parameters.AddWithValue("@clockoutevent", updatedTimeEvent.ClockOut);
            cmd.Parameters.AddWithValue("@eventdepartment", updatedTimeEvent.DepartmentId);
            cmd.Parameters.AddWithValue("@eventdate",updatedTimeEvent.Date);
            cmd.Parameters.AddWithValue("@clockinevent",updatedTimeEvent.ClockIn);
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

        public void ClockingOut(TimeEvent updatedTimeEvent)
        {
            System.Console.WriteLine("Clocking out...");

            IFindRecentTimeEvent myFinder = new FindMostRecentTimeEvent();
            
            IReadDepByName myDepFinder = new FindDepartmentByName();

            using var con = new MySqlConnection(cs);
            con.Open();

            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @"UPDATE timekeepingevents
                SET clockedoutcheck = 'y', 
                    clockoutevent = @clockoutevent, 
                    eventdepartment = @eventdepartment,
                    eventdate = @eventdate
                WHERE eventid = @eventid;";

            cmd.Parameters.AddWithValue("@eventid", myFinder.FindTimeEvent().TimeEventId);
            cmd.Parameters.AddWithValue("@clockoutevent", DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("@eventdate", DateTime.Now.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@eventdepartment", myDepFinder.Find(updatedTimeEvent.Department).DepId);
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