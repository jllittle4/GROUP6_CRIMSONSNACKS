using api.Models;

namespace api.Interfaces
{
    public interface IReportTimeEvents
    {
        public List<TimeEvent> Find(ReportRequest myReportRequest);
    }
}