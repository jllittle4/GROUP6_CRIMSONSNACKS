using api.Models;
using api.database;
using MySql.Data.MySqlClient;
using api.Interfaces;
using api.Adapters;

namespace api.Utilities
{
    public class ReportTotalTimeByDep : IReportTotalTime
    {
        public List<Report> myList = new List<Report>();
        private string cs { get; }
        public ReportTotalTimeByDep()
        {
            ConnectionString connectionString = new ConnectionString();
            this.cs = connectionString.cs;
        }

        public List<Report> Find(int id)
        {
            System.Console.WriteLine("Reading time events for an employee...");
            //System.Console.WriteLine(myReportRequest.Employee + "attempt to read employee id");

            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"SELECT departmentname, 
                    SUM(ROUND(TIMESTAMPDIFF(minute, clockinevent, clockoutevent)/60,2)) AS sum_hours
                FROM timekeepingevents tke JOIN departments d ON(tke.eventdepartment = d.departmentid)
                WHERE month(eventdate) = @eventdate
                GROUP BY d.departmentid;";

            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@eventdate", id);
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
                //System.Console.WriteLine(myList[0].ToString());
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Time events retrieval was unsuccessful.");
                System.Console.WriteLine("The following error was returned...");
                System.Console.WriteLine(e.ToString());
            }

            //con.Close();
            return myList;
        }
    }
}