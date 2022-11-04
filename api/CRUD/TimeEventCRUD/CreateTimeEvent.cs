using api.Interfaces;
using api.Models;
using api.Database;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace api.CRUD
{
    public class CreateTimeEvent : ICreateOneTimeEvent, IFormatDate
    {
        //connection to mysql database
        private string cs { get; }
        public DateTime myDT { get; set; }
        public TimeEvent myTimeEvent { get; set; }
        
        public CreateTimeEvent()
        {
            ConnectionString myCS = new ConnectionString();
            cs = myCS.cs;
        }

        //create time event based on incoming time event parameters
        //used primarily for creating new time events from time change requests
        public void CreateOneTimeEvent(TimeEvent newEvent)
        {
            myTimeEvent = newEvent;

            System.Console.WriteLine("The following time event will be created...");
            System.Console.WriteLine(newEvent.ToString());

            using var con = new MySqlConnection(cs);
            con.Open();

            var stm = @"INSERT INTO timekeepingevents (eventid, eventdate, clockinevent, clockoutevent, eventdepartment, eventemployee, clockedoutcheck) 
                VALUES (default, @eventdate, @clockinevent, @clockoutevent, @eventdepartment, @eventemployee, 'y');";

            using var cmd = new MySqlCommand(stm, con);

            cmd.Parameters.AddWithValue("@eventemployee", newEvent.EmployeeId);
            cmd.Parameters.AddWithValue("@eventdate", newEvent.Date);
            cmd.Parameters.AddWithValue("@clockoutevent", newEvent.ClockOut);
            cmd.Parameters.AddWithValue("@clockinevent", newEvent.ClockIn);
            cmd.Parameters.AddWithValue("@eventdepartment", newEvent.DepartmentId);
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("The time event has been created.");
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Time event creation was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.Message);
            }
        }

        //formats date of incoming time events to match required database formats
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