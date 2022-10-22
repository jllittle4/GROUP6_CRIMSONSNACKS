using MySql.Data.MySqlClient;
using api.Models;
using api.Interfaces;
using api.database;

namespace api.CRUD
{
    public class ReadTimeEvents : IReadAllTimeEvents, IReadOneTimeEvent
    {
        private string cs { get; }
        public ReadTimeEvents()
        {
            ConnectionString myCS = new ConnectionString();
            cs = myCS.cs;
        }
        public List<TimeEvent> ReadAllTimeEvents()
        {
            List<TimeEvent> allTimeEvents = new List<TimeEvent>();

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"SELECT eventid, 
                    DATE_FORMAT(eventdate, '%M %e, %Y') AS format_eventdate, 
                    TIME_FORMAT(clockinevent,'%I:%i %p') AS format_eventclockin, 
                    TIME_FORMAT(clockoutevent,'%I:%i %p') AS format_eventclockout, 
                    eventdepartment, eventemployee,
                    TIME_FORMAT(TIMEDIFF(clockoutevent, clockinevent),'%I:%i') AS totaltime 
                FROM timekeepingevents;";
            using var cmd = new MySqlCommand(stm, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                TimeEvent temp = new TimeEvent() 
                { 
                    TimeEventId = rdr.GetInt32(0), 
                    Date = rdr.GetString(1), 
                    ClockIn = rdr.GetString(2), 
                    ClockOut = rdr.GetString(3), 
                    DepartmentId = rdr.GetInt32(4), 
                    EmployeeId = rdr.GetInt32(5), 
                    TotalTime = rdr.GetString(6) 
                };

                allTimeEvents.Add(temp);
            }

            //con.Close();

            return allTimeEvents;
        }

        public TimeEvent ReadOneTimeEvent(int id)
        {
            System.Console.WriteLine("Looking for time event...");

            TimeEvent myTimeEvent = new TimeEvent();

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"SELECT eventid, 
                    DATE_FORMAT(eventdate, '%M %e, %Y') AS format_eventdate, 
                    TIME_FORMAT(clockinevent,'%I:%i %p') AS format_eventclockin, 
                    TIME_FORMAT(clockoutevent,'%I:%i %p') AS format_eventclockout, 
                    eventdepartment, eventemployee,
                    TIME_FORMAT(TIMEDIFF(clockoutevent, clockinevent),'%I:%i') AS totaltime
                FROM timekeepingevents
                WHERE eventid = @eventid;";

            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@eventid", id);
            cmd.Prepare();

            try
            {
                using MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    myTimeEvent.TimeEventId = rdr.GetInt32(0);
                    myTimeEvent.Date = rdr.GetString(1);
                    myTimeEvent.ClockIn = rdr.GetString(2);
                    myTimeEvent.ClockOut = rdr.GetString(3);
                    myTimeEvent.DepartmentId = rdr.GetInt32(4);
                    myTimeEvent.EmployeeId = rdr.GetInt32(5);
                    myTimeEvent.TotalTime = rdr.GetString(6);
                }
                System.Console.WriteLine("The result of the search was: ");
                System.Console.WriteLine(myTimeEvent.ToString());
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Time event search was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.ToString());
            }

            // con.Close();
            return myTimeEvent;
        }
    }
}