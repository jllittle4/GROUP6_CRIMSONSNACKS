using api.Models;
using api.Database;
using api.Interfaces;
using api.Adapters;
using MySql.Data.MySqlClient;

namespace api.Utilities
{
    public class ReportTotalTimeByDep : IReportTotalTime
    {
        //connection string to mysql database
        private string cs { get; }
        public List<Report> myList = new List<Report>();
        
        public ReportTotalTimeByDep()
        {
            ConnectionString connectionString = new ConnectionString();
            this.cs = connectionString.cs;
        }

        //returns list of report objects based on payroll period (month)
        public List<Report> Find(string date)
        {
            System.Console.WriteLine("Reading time events for an employee...");

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"SELECT departmentname, 
                    SUM(ROUND(TIMESTAMPDIFF(minute, clockinevent, clockoutevent)/60,2)) AS sum_hours
                FROM timekeepingevents tke JOIN departments d ON(tke.eventdepartment = d.departmentid)
                WHERE month(eventdate) = @eventdate
                GROUP BY d.departmentid;";

            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@eventdate", date);
            cmd.Prepare();

            try
            {
                using MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    DepartmentReport temp = new DepartmentReport();

                    temp.DepartmentName = rdr.GetString(0);
                    temp.TotalHours = rdr.GetDouble(1);

                    Report myAdapter = new DepartmentReportAdapter(temp);
                    myList.Add(myAdapter);
                }

                System.Console.WriteLine("Read time events by employee successfully.");
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Time events retrieval was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.ToString());
            }

            return myList;
        }
    }
}