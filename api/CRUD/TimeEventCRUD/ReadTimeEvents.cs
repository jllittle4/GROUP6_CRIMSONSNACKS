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
            System.Console.WriteLine("Reading all time events...");

            List<TimeEvent> allTimeEvents = new List<TimeEvent>();

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"SELECT eventid, 
                    DATE_FORMAT(eventdate, '%Y-%m-%d') AS format_eventdate,
                    TIME_FORMAT(clockinevent,'%I:%i %p') AS format_eventclockin, 
                    TIME_FORMAT(clockoutevent,'%I:%i %p') AS format_eventclockout, 
                    departmentname, eventemployee,
                    ROUND(TIMESTAMPDIFF(minute, clockinevent, clockoutevent)/60,2) AS totaltime,
                    clockedoutcheck 
                FROM timekeepingevents tke 
                    LEFT JOIN departments d ON(tke.eventdepartment = d.departmentid)
                ORDER BY eventdate DESC, clockinevent DESC;";

            using var cmd = new MySqlCommand(stm, con);

            try
            {
                using MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    TimeEvent temp = new TimeEvent();

                    temp.TimeEventId = rdr.GetInt32(0);
                    temp.Date = rdr.GetString(1);
                    temp.ClockIn = rdr.GetString(2);
                    temp.ClockOut = rdr.GetString(3);
                    //temp.DepartmentId = rdr.GetInt32(4);

                    // try
                    // {
                    //     temp.DepartmentId = rdr.GetInt32(4);
                    // }
                    // catch
                    // {
                    //     temp.DepartmentId = -1;
                    // }

                    try
                    {
                        temp.Department = rdr.GetString(4);
                    }
                    catch
                    {
                        temp.Department = "n/a";
                    }
                    temp.EmployeeId = rdr.GetInt32(5);
                    temp.TotalTime = rdr.GetString(6);
                    temp.ClockedOutCheck = rdr.GetString(7);


                    allTimeEvents.Add(temp);

                    //System.Console.WriteLine("Read all time events successfully.");
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Time events retrieval was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.ToString());
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
                    departmentname, eventemployee,
                    TIMESTAMPDIFF(minute, clockoutevent, clockinevent)/60 AS totaltime,
                    clockedoutcheck
                FROM timekeepingevents tke LEFT JOIN departments d ON(tke.eventdepartment = d.departmentid)
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

                    //myTimeEvent.DepartmentId = rdr.GetInt32(4);
                    // try
                    // {
                    //     myTimeEvent.DepartmentId = rdr.GetInt32(4);
                    // }
                    // catch
                    // {
                    //     myTimeEvent.DepartmentId = -1;
                    // }
                    try
                    {
                        myTimeEvent.Department = rdr.GetString(4);
                    }
                    catch
                    {
                        myTimeEvent.Department = "n/a";
                    }
                    //myTimeEvent.EmployeeId = rdr.GetInt32(5);
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