using api.Models;

namespace api.Interfaces
{
    public interface IHandleTimeEventFromReq
    {
        public Request FindRequest(Request myRequest);
    }
}