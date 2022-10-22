using MySql.Data.MySqlClient;
using api.Models;
using api.Interfaces;
using api.database;

namespace api.CRUD
{
    public class ReadRequests : IReadAllRequests, IReadOneRequest
    {
        private string cs { get; }
        public ReadRequests()
        {
            ConnectionString myCS = new ConnectionString();
            cs = myCS.cs;
        }
        public List<Request> ReadAllRequests()
        {
            List<Request> allRequests = new List<Request>();
            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"SELECT requestid, 
                    DATE_FORMAT(requestdate, '%M %e, %Y') AS format_requestdate, 
                    TIME_FORMAT(requestclockin,'%I:%i %p') AS format_requestclockin, 
                    TIME_FORMAT(requestclockout,'%I:%i %p') AS format_requestclockout, 
                    reason, requestdepartment, requestemployee, isapproved,
                    TIME_FORMAT(TIMEDIFF(requestclockout, requestclockin),'%I:%i') AS totaltime
                FROM requests
                ORDER BY requestid DESC;";

            using var cmd = new MySqlCommand(stm, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Request temp = new Request()
                {
                    RequestId = rdr.GetInt32(0),
                    Date = rdr.GetString(1),
                    ClockIn = rdr.GetString(2),
                    ClockOut = rdr.GetString(3),
                    Reason = rdr.GetString(4),
                    DepartmentId = rdr.GetInt32(5),
                    EmployeeId = rdr.GetInt32(6),
                    Status = rdr.GetString(7),
                    TotalTime = rdr.GetString(8)
                };
                allRequests.Add(temp);
            }

            //con.Close();

            return allRequests;
        }

        public Request ReadOneRequest(int id)
        {
            System.Console.WriteLine("Looking for request...");

            Request myRequest = new Request();

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"SELECT requestid, 
                    DATE_FORMAT(requestdate, '%M %e, %Y') AS format_requestdate, 
                    TIME_FORMAT(requestclockin,'%I:%i %p') AS format_requestclockin, 
                    TIME_FORMAT(requestclockout,'%I:%i %p') AS format_requestclockout, 
                    reason, requestdepartment, requestemployee, isapproved,
                    TIME_FORMAT(TIMEDIFF(requestclockout, requestclockin),'%I:%i') AS totaltime
                FROM requests 
                WHERE requestid = @id;";

            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            try
            {
                using MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    myRequest.RequestId = rdr.GetInt32(0);
                    myRequest.Date = rdr.GetString(1);
                    myRequest.ClockIn = rdr.GetString(2);
                    myRequest.ClockOut = rdr.GetString(3);
                    myRequest.Reason = rdr.GetString(4);
                    myRequest.DepartmentId = rdr.GetInt32(5);
                    myRequest.EmployeeId = rdr.GetInt32(6);
                    myRequest.Status = rdr.GetString(7);
                    myRequest.TotalTime = rdr.GetString(8);
                }
                System.Console.WriteLine("The result of the search was: ");
                System.Console.WriteLine(myRequest.ToString());
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Request search was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.ToString());
            }

            // con.Close();
            return myRequest;
        }
    }
}