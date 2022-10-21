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

            string stm = @"SELECT *
                FROM requests
                ORDER BY requestid DESC;";

            using var cmd = new MySqlCommand(stm, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Request temp = new Request() { RequestId = rdr.GetInt32(0), Date = rdr.GetDateTime(1), ClockIn = rdr.GetDateTime(2), ClockOut = rdr.GetDateTime(3), Reason = rdr.GetString(4), DepartmentId = rdr.GetInt32(5), EmployeeId = rdr.GetInt32(6), Status = rdr.GetString(7) };
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

            string stm = @"SELECT *
                FROM requests 
                WHERE requestid = @id";

            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            try
            {
                using MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    myRequest.RequestId = rdr.GetInt32(0);
                    myRequest.Date = rdr.GetDateTime(1);
                    myRequest.ClockIn = rdr.GetDateTime(2);
                    myRequest.ClockOut = rdr.GetDateTime(3);
                    myRequest.Reason = rdr.GetString(4);
                    myRequest.DepartmentId = rdr.GetInt32(5);
                    myRequest.EmployeeId = rdr.GetInt32(6);
                    myRequest.Status = rdr.GetString(7);
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