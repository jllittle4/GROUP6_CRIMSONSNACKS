using api.Interfaces;
using api.Models;
using api.database;
using MySql.Data.MySqlClient;

namespace api.CRUD
{
    public class DeleteRequest : IDeleteOne
    {
        private string cs;
        public DeleteRequest()
        {
            ConnectionString connectionString = new ConnectionString();
            cs = connectionString.cs;
        }
        public void DeleteOne(int id)
        {
            System.Console.WriteLine($"The request with an ID of {id} will be deleted");

            using var con = new MySqlConnection(cs);
            con.Open();

            using var cmd = new MySqlCommand();
            
            cmd.Connection = con;
            cmd.CommandText = @"DELETE FROM requests 
                WHERE requestid = @requestid;";

            cmd.Parameters.AddWithValue("@requestid", id);
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("The request has been deleted.");
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Request deletion was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.Message);
            }

            // con.Close();
        }
    }
}