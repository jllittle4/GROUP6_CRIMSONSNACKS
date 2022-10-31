using api.Models;

namespace api.Interfaces
{
    public interface IReportTotalTime
    {
        public List<Report> Find(int id);
    }
}