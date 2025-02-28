using Microsoft.AspNetCore.Mvc;
using UserApi.Models;

namespace UserApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<User> CreateUser([FromBody] User user);
    }
}
