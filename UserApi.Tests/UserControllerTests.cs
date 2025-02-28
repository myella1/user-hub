using Microsoft.Extensions.Logging;
using Moq;
using UserApi.Models;
using UserApi.Controllers;
using UserApi.Services;
using Microsoft.AspNetCore.Mvc;
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
        public async Task GetUsers_ReturnsOk()
        {
            // Arrange
            var users = new List<User> { new User { UserId = 1, FirstName = "Ashley", LastName = "Joy", EmailAddress = "ashley.joy@gmail.com" } };
            _userService.Setup(service => service.GetUsersAsync()).ReturnsAsync(users);

            // Act
            var result = await _controller.GetUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUsers = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
            Assert.Single(returnedUsers);
        }

        [Fact]
        public async Task GetUser_ReturnsOk()
        {
            // Arrange
            var user = new User { UserId = 1, FirstName = "Mike", LastName = "Smith", MiddleName = "N", EmailAddress = "MikeSmith@gmail.com" };
            _userService.Setup(s => s.GetUserAsync(1)).ReturnsAsync(user);

            // Act
            var result = await _controller.GetUser(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal(1, returnedUser.UserId);
        }

        [Fact]
        public async Task GetUser_ReturnsNotFound()
        {
            // Arrange
            _ = _userService.Setup(s => s.GetUserAsync(1)).ReturnsAsync((User)null);

            // Act
            var result = await _controller.GetUser(100);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateUser_ReturnsCreatedUser()
        {
            // Arrange
            var user = new User { UserId = 3, FirstName = "Mike", LastName = "Smith", MiddleName = "N", EmailAddress = "MikeSmith@gmail.com" };
            var expectedUsers = user;
            _userService.Setup(service => service.CreateUserAsync(user)).ReturnsAsync(expectedUsers);

            // Act
            var result = await _controller.CreateUser(user);

            // Assert
            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedUser = Assert.IsType<User>(createdAtResult.Value);
            Assert.Equal(3, returnedUser.UserId);
            Assert.Equal("Mike", returnedUser.FirstName);
        }
    }
}
