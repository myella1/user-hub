using Microsoft.AspNetCore.Mvc;
using userApi.Models;

namespace UserApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<IEnumerable<User>> CreateUser([FromBody] User user);
    }
}
