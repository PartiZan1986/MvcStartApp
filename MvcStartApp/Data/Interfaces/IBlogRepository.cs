using MvcStartApp.Models.Db;

namespace MvcStartApp.Data.Interfaces
{
    public interface IBlogRepository
    {
        Task AddUser(User user);
        Task<User[]> GetUsers();
    }
}
