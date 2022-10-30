using api.Models;
using api.Interfaces;
using MySql.Data.MySqlClient;
using api.database;
using api.Controllers;

namespace api.Utilities
{
    public class FindMostRecentTimeEvent : IFindRecentTimeEvent
    {
        public TimeEvent myTimeEvent = new TimeEvent();
        private string cs { get; }
        public FindMostRecentTimeEvent()
        {
            ConnectionString connectionString = new ConnectionString();
            this.cs = connectionString.cs;
        }

        public TimeEvent FindTimeEvent()
        {
            System.Console.WriteLine("Looking for most recent time event...\n");

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"SELECT eventid, 
                    DATE_FORMAT(eventdate, '%M %e, %Y') AS format_eventdate, 
                    TIME_FORMAT(clockinevent,'%I:%i %p') AS format_eventclockin, 
                    TIME_FORMAT(clockoutevent,'%I:%i %p') AS format_eventclockout, 
                    departmentname, eventemployee,
                    TIMESTAMPDIFF(minute, clockoutevent, clockinevent)/60 AS totaltime,
                    clockedoutcheck
                FROM timekeepingevents tke 
                    LEFT JOIN departments d ON(tke.eventdepartment = d.departmentid) 
                    JOIN employees e ON(tke.eventemployee = e.employeeid)
                WHERE username = @username
                ORDER BY eventdate DESC, clockinevent DESC
                LIMIT 1;";

            using var cmd = new MySqlCommand(stm, con);

            cmd.Parameters.AddWithValue("@username", Users.loggedIn.UserName);
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

                    try
                    {
                        myTimeEvent.Department = rdr.GetString(4);
                    }
                    catch
                    {
                        myTimeEvent.Department = "n/a";
                    }

                    try
                    {
                        myTimeEvent.EmployeeId = rdr.GetInt32(5);
                    }
                    catch
                    {
                        myTimeEvent.EmployeeId = -1;
                    }

                    myTimeEvent.TotalTime = rdr.GetString(6);
                    myTimeEvent.ClockedOutCheck = rdr.GetString(7);
                }

                System.Console.WriteLine("The result of the search was: \n");
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