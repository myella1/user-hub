using Microsoft.AspNetCore.Mvc;
using UserApi.Models;

namespace UserApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserAsync(int userId);
        Task<User> CreateUserAsync([FromBody] User user);
    }
}
