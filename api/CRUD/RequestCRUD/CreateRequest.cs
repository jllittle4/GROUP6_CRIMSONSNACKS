using api.Interfaces;
using api.database;
using MySql.Data.MySqlClient;
using api.Models;

namespace api.CRUD
{
    public class CreateRequest : ICreateOneRequest
    {
        private string cs { get; }
        public CreateRequest()
        {
            ConnectionString myCS = new ConnectionString();
            cs = myCS.cs;
        }

        public void CreateOneRequest(Request newRequest)
        {
            using var con = new MySqlConnection(cs);
            con.Open();

            var stm = @"INSERT INTO requests (requestid, requestdate, requestclockin, requestclockout, reason, requestdepartment, requestemployee, isapproved) 
                    VALUES (default,@requestdate,@requestclockin,@requestclockout,@reason,@requestdepartment,@requestemployee,default);";
            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@requestdate", newRequest.Date);
            cmd.Parameters.AddWithValue("@requestclockin", newRequest.ClockIn);
            cmd.Parameters.AddWithValue("@requestclockout", newRequest.ClockOut);
            cmd.Parameters.AddWithValue("@reason", newRequest.Reason);
            cmd.Parameters.AddWithValue("@requestdepartment", newRequest.DepartmentId);
            cmd.Parameters.AddWithValue("@requestemployee", newRequest.EmployeeId);
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("The request has been created.");
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Request creation was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.ToString());
            }
            //con.Close();
        }
    }
}