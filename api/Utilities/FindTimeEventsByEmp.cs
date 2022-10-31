using api.Controllers;
using api.Interfaces;
using api.Models;
using api.database;
using MySql.Data.MySqlClient;

namespace api.Utilities
{

    public class FindTimeEventsByEmp : IReadAllTimeEvents
    {
        public List<TimeEvent> mySingleUserList = new List<TimeEvent>();
        private string cs { get; }
        public FindTimeEventsByEmp()
        {
            ConnectionString connectionString = new ConnectionString();
            this.cs = connectionString.cs;
        }

        public List<TimeEvent> ReadAllTimeEvents()
        {
            System.Console.WriteLine("Reading time events for an employee...");


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
                    JOIN employees e ON(tke.eventemployee = e.employeeid)
                WHERE username = @username
                ORDER BY eventdate DESC, clockinevent DESC;";

            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@username", LoggingIn.loggedIn.UserName);
            cmd.Prepare();

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


                    mySingleUserList.Add(temp);

                }

                System.Console.WriteLine("Read time events by employee successfully.");
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Time events retrieval was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.ToString());
            }

            //con.Close();

            return mySingleUserList;
        }
    }
}