using Microsoft.Extensions.Logging;
using Moq;
using UserApi.Models;
using UserApi.Controllers;
using UserApi.Services;
namespace UserApi.Tests
{
    public class UserControllerTests
    {
        private UserController _controller;
        private Mock<IUserService> _userService;
        private Mock<ILogger<UserController>> _mockLogger;
        public UserControllerTests()
        {
            _mockLogger = new Mock<ILogger<UserController>>();
            _userService = new Mock<IUserService>();
            _controller = new UserController(_mockLogger.Object, _userService.Object);
        }

        [Fact]
        public async Task GetUsers_ReturnsUsers()
        {
            // Arrange
            var expectedUsers = new List<User> { new User { UserId = 1, FirstName = "Ashley", LastName = "Joy", EmailAddress = "ashley.joy@gmail.com" } };
            _userService.Setup(service => service.GetUsers()).ReturnsAsync(expectedUsers);

            // Act
            var result = await _controller.GetUsers();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(expectedUsers, result);
        }

        [Fact]
        public async Task GetUserById_ReturnsUser()
        {
            // Arrange
            int userId = 1;
            var expectedUser = new User { UserId = userId, FirstName = "John", LastName = "Doe", EmailAddress = "john.doe@example.com" };
            _userService.Setup(service => service.GetUser(userId)).ReturnsAsync(expectedUser);

            // Act
            var result = await _controller.GetUser(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser, result);
        }

        [Fact]
        public async Task CreateUser_ReturnsCreatedUser()
        {
            // Arrange
            var user = new User { UserId = 1, FirstName = "Jane", LastName = "Doe", EmailAddress = "jane.doe@example.com" };
            var expectedUsers = user;
            _userService.Setup(service => service.CreateUser(user)).ReturnsAsync(expectedUsers);

            // Act
            var result = await _controller.CreateUser(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUsers, result);
        }
    }
}
