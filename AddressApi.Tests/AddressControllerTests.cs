using AddressApi.Controllers;
using AddressApi.Models;
using AddressApi.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace AddressApi.Tests
{
    public class AddressControllerTests
    {
        private AddressController _controller;
        private Mock<IAddressService> _addressService;
        private Mock<ILogger<AddressController>> _mockLogger;
        public AddressControllerTests()
        {
            _mockLogger = new Mock<ILogger<AddressController>>();
            _addressService = new Mock<IAddressService>();
            _controller = new AddressController(_addressService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAddress_ReturnsAddresses()
        {
            // Arrange
            var userId = 1;
            var expectedAddresses = new List<Address> { new Address { AddressId = 1, UserId = userId, Address1 = "123 Main St", City = "City", State = "State", PostalCode = "12345", AddressType = AddressType.Billing } };
            _addressService.Setup(service => service.GetAddresses()).ReturnsAsync(expectedAddresses);

            // Act
            var result = await _controller.GetAddresses();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(expectedAddresses, result);
        }

        [Fact]
        public async Task GetAddress_ReturnsAddress()
        {
            // Arrange
            var addressId = 1;
            var expectedAddress = new Address { AddressId = 1, UserId = 1, Address1 = "5214 Madison Ave", Address2 = "", City = "Chicago", State = "IL", PostalCode = "50678", AddressType = AddressType.Billing };
            _addressService.Setup(service => service.GetAddress(addressId)).ReturnsAsync(expectedAddress);

            // Act
            var result = await _controller.GetAddress(addressId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAddress, result);
        }

        [Fact]
        public async Task GetAddress_ReturnsNull_OnException()
        {
            // Arrange
            _addressService.Setup(service => service.GetAddresses()).ThrowsAsync(new System.Exception("Database error"));

            // Act
            var result = await _controller.GetAddresses();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAddress_ReturnsCreatedAddress()
        {
            // Arrange
            var address = new Address { AddressId = 1, UserId = 1, Address1 = "456 Elm St", City = "City", State = "State", PostalCode = "67890", AddressType = AddressType.Shipping };
            var expectedAddresses = address;
            _addressService.Setup(service => service.CreateAddress(address)).ReturnsAsync(expectedAddresses);

            // Act
            var result = await _controller.CreateAddress(address);

            // Assert
            Assert.Equal(expectedAddresses, result);
        }

        [Fact]
        public async Task CreateAddress_ReturnsNull_OnException()
        {
            // Arrange
            var address = new Address { AddressId = 1, UserId = 1, Address1 = "456 Elm St", City = "City", State = "State", PostalCode = "67890", AddressType = AddressType.Shipping };
            _addressService.Setup(service => service.CreateAddress(address)).ThrowsAsync(new System.Exception("Database error"));

            // Act
            var result = await _controller.CreateAddress(address);

            // Assert
            Assert.Null(result);
        }
    }
}
