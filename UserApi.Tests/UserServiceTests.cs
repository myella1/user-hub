using Microsoft.Extensions.Logging;
using Moq;
using UserApi.Models;
using UserApi.Services;

namespace UserApi.Tests
{
    public class UserServiceTests
    {
        private readonly UserService _service;
        private readonly Mock<ILogger<UserService>> _mockLogger;

        public UserServiceTests()
        {
            _mockLogger = new Mock<ILogger<UserService>>();
            _service = new UserService(_mockLogger.Object);
        }

        [Fact]
        public async Task GetUsers_ReturnsAllUsers()
        {
            // Act
            var result = await _service.GetUsersAsync();

            // Assert
            Assert.NotNull(result);
            var users = result.ToList();
            Assert.Equal(2, users.Count);
        }

        [Fact]
        public async Task GetUser_ReturnsCorrectUser()
        {
            // Arrange
            int userId = 1;

            // Act
            var result = await _service.GetUserAsync(userId);

            // Assert
            Assert.NotNull(result); 
            Assert.Equal("Mike", result?.FirstName);
            Assert.Equal("Smith", result?.LastName);
        }

        [Fact]
        public async Task GetUser_ReturnsNullForNonExistentUser()
        {
            // Arrange
            int nonExistentUserId = 99;

            // Act
            var result = await _service.GetUserAsync(nonExistentUserId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateUser_AddsUserAndReturnsUser()
        {
            // Arrange
            var newUser = new User
            {
                UserId = 3,
                FirstName = "John",
                LastName = "Doe",
                MiddleName = "B",
                EmailAddress = "JohnDoe@gmail.com"
            };

            // Act
            var result = await _service.CreateUserAsync(newUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newUser, result);
        }
    }
}