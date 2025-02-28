using UserGatewayApi.Controllers;
using UserGatewayApi.Models;
using UserGatewayApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;

namespace UserGatewayApi.Tests
{
    public class GatewayControllerTests
    {
        private GatewayController _controller;
        private Mock<IGatewayService> _gatewayService; 
        private readonly Mock<ILogger<GatewayController>> _logger;

        public GatewayControllerTests()
        {
            _gatewayService = new Mock<IGatewayService>();
            _logger = new Mock<ILogger<GatewayController>>();
            _controller = new GatewayController(_gatewayService.Object, _logger.Object);
        }

        [Fact]
        public async Task GetData_ReturnsOkResult()
        {
            // Arrange
            var users = new List<User> { new User { UserId = 1, FirstName = "Ashley", LastName = "Joy", EmailAddress = "ashley.joy@gmail.com" } };
            var addresses = new List<Address> { new Address { AddressId = 1, UserId = 1, Address1 = "5214 Madison Ave", Address2 = "", City = "Chicago", State = "IL", PostalCode = "50678", AddressType = AddressType.Billing } };

            _gatewayService.Setup(s => s.GetUsersAsync()).ReturnsAsync(users);
            _gatewayService.Setup(s => s.GetAddressesAsync()).ReturnsAsync(addresses);

            // Act
            var result = await _controller.GetData();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }
        [Fact]
        public async Task CreateUser_ReturnsOk_WithCreatedUser()
        {
            // Arrange
            var user = new User { UserId = 1, FirstName = "Ashley", LastName = "Joy", EmailAddress = "ashley.joy@gmail.com" } ;
            _gatewayService.Setup(s => s.CreateUserAsync(It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await _controller.CreateUser(user);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<User>(okResult.Value);
            Assert.Equal(user.UserId, response.UserId);
            Assert.Equal(user.FirstName, response.FirstName);
        }

        [Fact]
        public async Task CreateAddress_ReturnsOk_WithCreatedAddress()
        {
            // Arrange
            var address = new Address { AddressId = 1, UserId = 1, Address1 = "5214 Madison Ave", Address2 = "", City = "Chicago", State = "IL", PostalCode = "50678", AddressType = AddressType.Billing };
            _gatewayService.Setup(s => s.CreateAddressAsync(It.IsAny<Address>())).ReturnsAsync(address);

            // Act
            var result = await _controller.CreateAddress(address);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<Address>(okResult.Value);
            Assert.Equal(address.AddressId, response.AddressId);
            Assert.Equal(address.Address1, response.Address1);
        }
    }
}