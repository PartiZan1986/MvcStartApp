using MvcStartApp.Models.Db;

namespace MvcStartApp.Data.Interfaces
{
    public interface IRequestRepository
    {
        Task LogRequest(RequestLog request);
        Task<RequestLog[]> GetRequests();
    }
}
