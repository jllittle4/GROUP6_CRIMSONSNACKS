using api.Interfaces;
using api.Database;
using MySql.Data.MySqlClient;

namespace api.CRUD
{
    public class DeleteRequest : IDeleteOne
    {
        //connection string to mysql database
        private string cs;
        public DeleteRequest()
        {
            ConnectionString connectionString = new ConnectionString();
            cs = connectionString.cs;
        }

        //deletes time change request
        //currently not used
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
        }
    }
}