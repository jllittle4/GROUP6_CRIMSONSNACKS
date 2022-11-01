using api.Models;
using api.database;
using MySql.Data.MySqlClient;
using api.Interfaces;
using api.Adapters;

namespace api.Utilities
{
    public class ReportTotalTimeByEmp : IReportTotalTime
    {
        public List<Report> myList = new List<Report>();
        private string cs { get; }
        public ReportTotalTimeByEmp()
        {
            ConnectionString connectionString = new ConnectionString();
            this.cs = connectionString.cs;
        }
        public List<Report> Find(string date)
        {
            System.Console.WriteLine("Reading time events for an employee...");
            //System.Console.WriteLine(myReportRequest.Employee + "attempt to read employee id");

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"SELECT CONCAT(firstname, ' ', lastname) as employee_name, 
                    SUM(ROUND(TIMESTAMPDIFF(minute, clockinevent, clockoutevent)/60,2)) AS sum_hours
                FROM timekeepingevents tke JOIN employees e ON(tke.eventemployee = e.employeeid)
                WHERE MONTH(eventdate) = @eventdate
                GROUP BY e.employeeid;";

            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@eventdate", date);
            cmd.Prepare();

            try
            {
                using MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    EmployeeReport temp = new EmployeeReport();

                    temp.FullName = rdr.GetString(0);
                    temp.TotalHours = rdr.GetDouble(1);

                    Report myAdapter = new EmployeeReportAdapter(temp);
                    myList.Add(myAdapter);
                }

                System.Console.WriteLine("Read total time by employee report successfully.");
                //System.Console.WriteLine(myList[0].ToString());
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Total time by employee report was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.ToString());
            }

            //con.Close();
            return myList;
        }
    }
}