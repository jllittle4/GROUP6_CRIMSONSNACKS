using api.Interfaces;
using api.Database;
using api.Models;
using api.Controllers;
using api.Utilities;
using MySql.Data.MySqlClient;

namespace api.CRUD
{
    public class CreateRequest : ICreateOneRequest
    {
        //connection string to mysql database
        private string cs { get; }
        public IReadDepByName myDepFinder = new FindDepartmentByName();

        public CreateRequest()
        {
            ConnectionString myCS = new ConnectionString();
            cs = myCS.cs;
        }

        //creates new request with auto incremented request id, date for requested change, requested clock in change, requested clockout change, requested department change, reason for change, and currently logged in user id
        public void CreateOneRequest(Request newRequest)
        {
            System.Console.WriteLine("The following request will be created...");
            System.Console.WriteLine(newRequest.ToString());

            using var con = new MySqlConnection(cs);
            con.Open();

            var stm = @"INSERT INTO requests (requestid, requestdate, requestclockin, requestclockout, reason, requestdepartment, requestemployee, isapproved) 
                    VALUES (default,@requestdate,@requestclockin,@requestclockout,@reason,@requestdepartment,@requestemployee,default);";

            using var cmd = new MySqlCommand(stm, con);

            cmd.Parameters.AddWithValue("@requestdate", newRequest.Date);
            cmd.Parameters.AddWithValue("@requestclockin", newRequest.ClockIn);
            cmd.Parameters.AddWithValue("@requestclockout", newRequest.ClockOut);
            cmd.Parameters.AddWithValue("@reason", newRequest.Reason);
            cmd.Parameters.AddWithValue("@requestdepartment", myDepFinder.Find(newRequest.Department).DepId);
            cmd.Parameters.AddWithValue("@requestemployee", LoggingIn.loggedIn.UserId);
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
        }
    }
}