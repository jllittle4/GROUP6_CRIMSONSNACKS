using api.Interfaces;
using MySql.Data.MySqlClient;
using api.Models;
using api.database;
using api.Utilities;

namespace api.CRUD
{
    public class CreateTimeEvent : ICreateOneTimeEvent
    {
        private string cs { get; }
        public CreateTimeEvent()
        {
            ConnectionString myCS = new ConnectionString();
            cs = myCS.cs;
        }
        public void CreateOneTimeEvent(TimeEvent clockInEvent)
        {
            System.Console.WriteLine("The following time event will be created...");
            System.Console.WriteLine(clockInEvent.ToString());

            using var con = new MySqlConnection(cs);
            con.Open();

            var stm = @"INSERT INTO timekeepingevents (eventid, eventdate, clockinevent, clockoutevent, eventdepartment, eventemployee, clockedoutcheck) 
                VALUES (default, @eventdate, @clockinevent, @clockoutevent, null, @eventemployee, default);";

            using var cmd = new MySqlCommand(stm, con);
        
            //cmd.Parameters.AddWithValue("@eventdepartment", clockInEvent.DepartmentId);
            cmd.Parameters.AddWithValue("@eventemployee", clockInEvent.EmployeeId);
            cmd.Parameters.AddWithValue("@eventdate",DateTime.Now.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@clockoutevent",DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("@clockinevent",DateTime.Now.ToString("HH:mm:ss"));
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
            
            //con.Close();
        }

        public void ClockingIn()
        {
            System.Console.WriteLine("Clocking in...");

            FindUserByUsername myFinder = new FindUserByUsername();
            User myUser = myFinder.Find();

            using var con = new MySqlConnection(cs);
            con.Open();

            var stm = @"INSERT INTO timekeepingevents (eventid, eventdate, clockinevent, clockoutevent, eventdepartment, eventemployee, clockedoutcheck) 
                VALUES (default, @eventdate, @clockinevent, @clockoutevent, null, @eventemployee, default);";

            using var cmd = new MySqlCommand(stm, con);
        
            //cmd.Parameters.AddWithValue("@eventdepartment", clockInEvent.DepartmentId);
            cmd.Parameters.AddWithValue("@eventemployee", myUser.UserId);
            cmd.Parameters.AddWithValue("@eventdate",DateTime.Now.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@clockoutevent",DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("@clockinevent",DateTime.Now.ToString("HH:mm:ss"));
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
            
            //con.Close();
        }
    }
}