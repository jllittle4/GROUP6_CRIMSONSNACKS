using api.Interfaces;
using api.Models;
using api.Database;
using MySql.Data.MySqlClient;
using api.Utilities;
using System.Globalization;

namespace api.Utilities
{
    public class ClockOut : IClockOut
    {
        //connection string to mysql database
        private string cs;
        public IFindRecentTimeEvent myFinder = new FindMostRecentTimeEvent();
        public IReadDepByName myDepFinder = new FindDepartmentByName();
        
        public ClockOut()
        {
            ConnectionString connectionString = new ConnectionString();
            cs = connectionString.cs;
        }

        //finds most recent time event for the currently logged in employee
        //gets department id of selection from department dropdown
        //updates the most recent time event with that department, the current time, the current date, and marks the time event as clocked out to alert the frontend that the next time event action will be a clockin
        public void ClockingOut(TimeEvent updatedTimeEvent)
        {
            System.Console.WriteLine("Clocking out...");

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
        }
    }
}