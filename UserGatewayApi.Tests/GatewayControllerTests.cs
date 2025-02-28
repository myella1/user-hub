using UserGatewayApi.Controllers;
using UserGatewayApi.Models;
using UserGatewayApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UserGatewayApi.Tests
{
    public class GatewayControllerTests
    {
        private GatewayController _controller;
        private Mock<IGatewayService> _gatewayService;
        public GatewayControllerTests()
        {
            _gatewayService = new Mock<IGatewayService>();
            _controller = new GatewayController(_gatewayService.Object);
        }

        [Fact]
        public async Task GetData_ReturnsOkResult()
        {
            // Arrange
            var mockUsers = new List<User>
            {
                new User { UserId = 1, FirstName = "Kim", LastName = "Joy", EmailAddress = "kim@gmail.com" }
            };

            var mockAddresses = new List<Address>
            {
                new Address { AddressId = 1, UserId = 1, Address1 = "123 Street", City = "City", State = "State", PostalCode = "12345", AddressType = AddressType.Billing }
            };

            _gatewayService.Setup(x => x.GetUsersAsync()).ReturnsAsync(mockUsers);
            _gatewayService.Setup(x => x.GetAddressesAsync()).ReturnsAsync(mockAddresses);

            // Act
            var result = await _controller.GetData();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }


        [Fact]
        public async Task CreateUser_ReturnsCreatedUser()
        {
            // Arrange
            var user = new User { UserId = 1, FirstName = "Kim", LastName = "Joy", EmailAddress = "kim@gmail.com" };
            _gatewayService.Setup(s => s.CreateUserAsync(user)).ReturnsAsync(user);

            // Act
            var result = await _controller.CreateUser(user);

            // Assert
            Assert.Equal(user, result);
        }


        [Fact]
        public async Task CreateAddress_ReturnsCreatedAddress()
        {
            // Arrange
            var address = new Address { AddressId = 1, UserId = 1, Address1 = "123 Main St", City = "City", State = "State", PostalCode = "12345", AddressType = AddressType.Billing };
            _gatewayService.Setup(s => s.CreateAddressAsync(address)).ReturnsAsync(address);

            // Act
            var result = await _controller.CreateAddress(address);

            // Assert
            Assert.Equal(address, result);
        }

    }
}