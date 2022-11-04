using api.Interfaces;
using api.Models;
using api.Database;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace api.CRUD
{
    public class UpdateTimeEvent : IUpdateOneTimeEvent, IFormatDate
    {
        //connection to mysql database
        private string cs;
        public DateTime myDT { get; set; }
        public TimeEvent myTimeEvent { get; set; }
        public UpdateTimeEvent()
        {
            ConnectionString connectionString = new ConnectionString();
            cs = connectionString.cs;
        }

        //updates time event to match incoming time event parameters
        //currently used to update time event to match time change request parameters
        public void UpdateOneTimeEvent(int id, TimeEvent updatedTimeEvent)
        {
            myTimeEvent = updatedTimeEvent;

            System.Console.WriteLine($"The time event with an ID of {id} will be updated to match the following details: ");
            System.Console.WriteLine(myTimeEvent.ToString());

            FormatDate();

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
            cmd.Parameters.AddWithValue("@clockoutevent", myTimeEvent.ClockOut);
            cmd.Parameters.AddWithValue("@eventdepartment", myTimeEvent.DepartmentId);
            cmd.Parameters.AddWithValue("@eventdate", myTimeEvent.Date);
            cmd.Parameters.AddWithValue("@clockinevent", myTimeEvent.ClockIn);
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

        //format dates and times to match required database date and time formats
        public void FormatDate()
        {
            try
            {
                myDT = DateTime.ParseExact(myTimeEvent.Date, "MMMM d, yyyy", new CultureInfo("en-US"));
                myTimeEvent.Date = myDT.ToString("yyyy-MM-dd");
            }
            catch
            {
                System.Console.WriteLine("Date did not need to be formatted.");
            }

            myDT = DateTime.ParseExact(myTimeEvent.ClockIn, "hh:mm tt", new CultureInfo("en-US"));
            myTimeEvent.ClockIn = myDT.ToString("HH:mm");

            myDT = DateTime.ParseExact(myTimeEvent.ClockOut, "hh:mm tt", new CultureInfo("en-US"));
            myTimeEvent.ClockOut = myDT.ToString("HH:mm");
        }
    }
}