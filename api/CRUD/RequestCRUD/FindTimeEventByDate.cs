using api.Models;
using api.Interfaces;
using api.Database;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace api.Utilities
{
    public class FindTimeEventByDate : IFindTimeEventByDate, IFormatDate
    {
        
        //connection string to mysql database
        private string cs { get; }
        public List<TimeEvent> myTimeEventsList = new List<TimeEvent>();
        public TimeEvent myTimeEvent = new TimeEvent();
        public Request myRequest { get; set; }

        public FindTimeEventByDate()
        {
            ConnectionString connectionString = new ConnectionString();
            this.cs = connectionString.cs;
        }

        //returns list of candidate time events that a time change request may update or overwrite based on currently logged in employee and requested date for time change
        public List<TimeEvent> Find(Request incomingRequest)
        {
            myRequest = incomingRequest;

            System.Console.WriteLine("Looking for most recent time event...\n");

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"SELECT eventid, 
                    DATE_FORMAT(eventdate, '%M %e, %Y') AS format_eventdate, 
                    TIME_FORMAT(clockinevent,'%I:%i %p') AS format_eventclockin, 
                    TIME_FORMAT(clockoutevent,'%I:%i %p') AS format_eventclockout, 
                    departmentname, 
                    eventemployee,
                    TIMESTAMPDIFF(minute, clockoutevent, clockinevent)/60 AS totaltime,
                    clockedoutcheck
                FROM timekeepingevents tke 
                    LEFT JOIN departments d ON(tke.eventdepartment = d.departmentid) 
                    JOIN employees e ON(tke.eventemployee = e.employeeid)
                WHERE eventemployee = @eventemployee AND eventdate = @eventdate
                ORDER BY eventdate DESC, clockinevent DESC;";

            using var cmd = new MySqlCommand(stm, con);

            cmd.Parameters.AddWithValue("@eventemployee", myRequest.EmployeeId);
            cmd.Parameters.AddWithValue("@eventdate", myRequest.Date);
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

                    myTimeEventsList.Add(myTimeEvent);
                }

                System.Console.WriteLine("The result of the search was: \n");
                System.Console.WriteLine(myTimeEventsList[0].ToString());
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Time event search was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.ToString());
            }

            return myTimeEventsList;
        }

        //formats data to be consistent with database query format
        public void FormatDate()
        {
            try
            {
                DateTime myDT = DateTime.ParseExact(myRequest.Date, "MMMM d, yyyy", new CultureInfo("en-US"));
                myRequest.Date = myDT.ToString("yyyy-MM-dd");
            }
            catch
            {
                System.Console.WriteLine("Date did not need to be formatted.");
            }
        }
    }
}