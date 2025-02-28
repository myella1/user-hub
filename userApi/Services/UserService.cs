using Microsoft.AspNetCore.Mvc;
using UserApi.Models;

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

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            _logger.LogInformation($"Retrieving all users");

            await Task.Delay(500);
            return _users.AsEnumerable();
        }

        public async Task<User?> GetUserAsync(int userId)
        {
            using (_logger.BeginScope(new Dictionary<string, object> { ["UserId"] = userId }))
            {
                _logger.LogInformation("Retrieving user info.");

                await Task.Delay(1000);
                var user = _users.FirstOrDefault(x => x.UserId == userId);

                if (user == null)
                {
                    _logger.LogWarning("User not found.");
                }

                return user;
            }
        }

        public async Task<User> CreateUserAsync([FromBody] User user)
        {
            var highestUserId = _users.Select(x => x.UserId).AsEnumerable().Distinct().Max();
            user.UserId = highestUserId+1;

            using (_logger.BeginScope(new Dictionary<string, object> { ["UserId"] = user.UserId! }))
            {
                await Task.Delay(1000); 
                _users.Add(user);

                _logger.LogInformation("User created successfully.");
            }

            return user;
        }
    }
}

