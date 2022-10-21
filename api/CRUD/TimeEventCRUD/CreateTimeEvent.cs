using api.Interfaces;
using MySql.Data.MySqlClient;
using api.Models;
using api.database;

namespace api.CRUD
{
    public class CreateTimeEvent : ICreateOneTimeEvent
    {
        //public User? temp {get; set;}
        private string cs { get; }
        public CreateTimeEvent()
        {
            ConnectionString myCS = new ConnectionString();
            cs = myCS.cs;
        }
        public void CreateOneTimeEvent(TimeEvent temp)
        {
            using var con = new MySqlConnection(cs);

            con.Open();
            var stm = @"INSERT INTO timekeepingevents (eventid, eventdate, clockinevent, clockoutevent, eventdepartment, eventemployee, clockedoutcheck) 
                VALUES (default, default, default, default, @eventdepartment, @eventemployee, default);";
            using var cmd = new MySqlCommand(stm, con);
        
            cmd.Parameters.AddWithValue("@eventdepartment", temp.DepartmentId);
            cmd.Parameters.AddWithValue("@eventemployee", temp.EmployeeId);
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
                System.Console.WriteLine(e.ToString());
            }
            //con.Close();
        }
    }
}