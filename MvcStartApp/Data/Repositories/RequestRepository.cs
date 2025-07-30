
using Microsoft.EntityFrameworkCore;
using MvcStartApp.Data.Interfaces;
using MvcStartApp.Data.Repositories;
using MvcStartApp.Models.Db;

namespace MvcStartApp.Data.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly BlogContext _context;

        public RequestRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task LogRequest(RequestLog request)
        {
            request.Id = Guid.NewGuid();
            request.Date = DateTime.Now;

            var entry = _context.Entry(request);
            if (entry.State == EntityState.Detached)
                await _context.RequestLogs.AddAsync(request);
                await _context.SaveChangesAsync();
        }

        public async Task<RequestLog[]> GetRequests()
        {
            return await _context.RequestLogs.ToArrayAsync();
        }

        public async Task<RequestLog[]> GetAllRequests()
        {
            return await _context.RequestLogs
                .OrderByDescending(r => r.Date)
                .ToArrayAsync();
        }
    }
}
