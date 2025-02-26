using Microsoft.AspNetCore.Mvc;
using userApi.Models;

namespace UserApi.Services
{
    public class UserService : IUserService
    {
        private static List<User> _users = new() {
                                                    new User { UserId = 1, FirstName = "Mike", LastName = "Smith", MiddleName = "N", EmailAddress = "MikeSmith@gmail.com" },
                                                    new User { UserId = 2, FirstName="Sarah", LastName="Jane", MiddleName="A", EmailAddress="SarahJane@gmail.com" }
                                                 };
        public async Task<IEnumerable<User>> GetUsers()
        {
            await Task.Delay(500);
            return _users;
        }
        public async Task<User> GetUser(int id)
        {
            await Task.Delay(1000);
            var user = _users.Where(x => x.UserId == id).FirstOrDefault();
            return user!;
        }
        public async Task<IEnumerable<User>> CreateUser([FromBody] User user)
        {
            await Task.Delay(1000);
            _users.Add(user);
            return _users;
        }
    }
}
