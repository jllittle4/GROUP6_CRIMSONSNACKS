using api.Interfaces;
using api.Models;
using api.database;
using MySql.Data.MySqlClient;

namespace api.CRUD
{
    public class UpdateUser : IUpdateOneUser
    {
        private string cs;
        public UpdateUser()
        {
            ConnectionString connectionString = new ConnectionString();
            cs = connectionString.cs;
        }
        public void UpdateOneUser(int id, User updatedUser)
        {
            System.Console.WriteLine($"The user with an ID of {id} will be updated to match the following details: ");
            System.Console.WriteLine(updatedUser.ToString());

            //double myRating = double.Parse(rating);
            using var con = new MySqlConnection(cs);
            con.Open();

            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @"UPDATE employees
                SET ismanager = @ismanager
                WHERE employeeid = @employeeid;";
            cmd.Parameters.AddWithValue("@ismanager", updatedUser.UserId);
            cmd.Parameters.AddWithValue("@employeeid", id);
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("User was updated successfully!");
            }
            catch (Exception e)
            {
                System.Console.WriteLine("User update was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.ToString());
            }

            // con.Close();
        }
    }
}