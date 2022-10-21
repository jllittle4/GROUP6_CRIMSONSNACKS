using api.Interfaces;
using api.Models;
using api.database;
using MySql.Data.MySqlClient;

namespace api.CRUD
{
    public class UpdateRequest : IUpdateOneRequest
    {
        private string cs;
        public UpdateRequest()
        {
            ConnectionString connectionString = new ConnectionString();
            cs = connectionString.cs;
        }
        public void UpdateOneRequest(int id, Request updatedRequest)
        {
            System.Console.WriteLine($"The request with an ID of {id} will be updated to match the following details: ");
            System.Console.WriteLine(updatedRequest.ToString());

            using var con = new MySqlConnection(cs);
            con.Open();

            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @"UPDATE requests 
                SET isapproved = @isapproved 
                WHERE requestid = @requestid";
            cmd.Parameters.AddWithValue("@isapproved", updatedRequest.Status);
            cmd.Parameters.AddWithValue("@requestid", id);
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("Request update was successful!");
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Request update was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.ToString());
            }

            // con.Close();
        }
    }
}