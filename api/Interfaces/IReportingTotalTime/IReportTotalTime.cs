using api.Models;

namespace api.Interfaces
{
    public interface IReportTotalTime
    {
        public List<Report> Find(string date);
    }
}