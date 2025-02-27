using Microsoft.AspNetCore.Mvc;
using userApi.Models;

namespace UserApi.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private static List<User> _users = new() {
                                                    new User { UserId = 1, FirstName = "Mike", LastName = "Smith", MiddleName = "N", EmailAddress = "MikeSmith@gmail.com" },
                                                    new User { UserId = 2, FirstName="Sarah", LastName="Jane", MiddleName="A", EmailAddress="SarahJane@gmail.com" }
                                                 };

        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            _logger.LogInformation($"Retrieving all the users");

            await Task.Delay(500);
            return _users.AsEnumerable();
        }
        public async Task<User> GetUser(int userId)
        {
            var loggingScope = new Dictionary<string, object>
            {
                ["UserId"] = userId
            };
            using var _ = _logger.BeginScope(loggingScope);
            _logger.LogInformation($"Retrieving user info for user: {userId}");

            await Task.Delay(1000);
            var user = _users.Where(x => x.UserId == userId).FirstOrDefault();
            return user!;
        }
        public async Task<User> CreateUser([FromBody] User user)
        {
            var loggingScope = new Dictionary<string, object>
            {
                ["UserId"] = user.UserId
            };
            using var _ = _logger.BeginScope(loggingScope);
            _logger.LogInformation($"Created user : {user.UserId}");

            await Task.Delay(1000);
            _users.Add(user);

            return user;
        }
    }
}
