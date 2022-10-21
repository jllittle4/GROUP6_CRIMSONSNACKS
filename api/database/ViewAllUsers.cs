using api.Interfaces;

using MySql.Data.MySqlClient;
using api.Models;


namespace api.database
{
    public class ViewAllUsers: IViewUsers
    {
        public List<User> GetAllUsers()
        {
            List<User> allUsers = new List<User>();

            ConnectionString myConnection = new ConnectionString();

            string cs = myConnection.cs;
            using var con = new MySqlConnection(cs);

            con.Open();

            string stm = @"SELECT employeeid, firstname, lastname, username, password FROM employees;";
            // WHERE deleted = '0' ORDER BY hire_date DESC
            using var cmd = new MySqlCommand(stm, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read()){
                User temp = new User(){UserId = rdr.GetInt32(0), FirstName = rdr.GetString(1), LastName = rdr.GetString(2), UserName = rdr.GetString(3), Password = rdr.GetString(4)};
                allUsers.Add(temp);
            }
            con.Close();

            return allUsers;
        }

        public static void ViewDriversTable(){
            // ConnectionString myConnection = new ConnectionString();

            // string cs = myConnection.cs;
            // using var con = new MySqlConnection(cs);

            // con.Open();

            // string stm = @"Select name, rating, hire_date, id from drivers WHERE deleted = '0' ORDER BY hire_date DESC";
            // using var cmd = new MySqlCommand(stm, con);

            // cmd.ExecuteNonQuery();
            // con.Close();
        }
    }
}