using AddressApi.Controllers;
using AddressApi.Models;
using AddressApi.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Mvc;

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
        public async Task GetAddresses_ReturnsOk()
        {
            // Arrange
            var addresses = new List<Address> { new Address { AddressId = 1, UserId = 1, Address1 = "5214 Madison Ave", Address2 = "", City = "Chicago", State = "IL", PostalCode = "50678", AddressType = AddressType.Billing } };
            _addressService.Setup(service => service.GetAddressesAsync()).ReturnsAsync(addresses);

            // Act
            var result = await _controller.GetAddresses();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedAddresses = Assert.IsAssignableFrom<IEnumerable<Address>>(okResult.Value);
            Assert.Single(returnedAddresses);
        }

        [Fact]
        public async Task GetAddress_ReturnsOk()
        {
            // Arrange
            var expectedAddress = new Address { AddressId = 1, UserId = 1, Address1 = "5214 Madison Ave", Address2 = "", City = "Chicago", State = "IL", PostalCode = "50678", AddressType = AddressType.Billing };
            _addressService.Setup(service => service.GetAddressAsync(1)).ReturnsAsync(expectedAddress);

            // Act
            var result = await _controller.GetAddress(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedAddress = Assert.IsType<Address>(okResult.Value);
            Assert.Equal(1, returnedAddress.AddressId);
        }

        [Fact]
        public async Task GetAddress_ReturnsNotFound()
        {
            // Arrange
            _ = _addressService.Setup(s => s.GetAddressAsync(1)).ReturnsAsync((Address)null);

            // Act
            var result = await _controller.GetAddress(1000);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }



        [Fact]
        public async Task CreateAddress_ReturnsCreatedAddress()
        {
            // Arrange
            var address = new Address { AddressId = 100, UserId = 1, Address1 = "5214 Madison Ave", Address2 = "", City = "Chicago", State = "IL", PostalCode = "50678", AddressType = AddressType.Billing };

            var expectedAddresses = address;
            _addressService.Setup(service => service.CreateAddressAsync(address)).ReturnsAsync(expectedAddresses);

            // Act
            var result = await _controller.CreateAddress(address);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedAddress = Assert.IsType<Address>(okResult.Value);
            Assert.Equal(100, returnedAddress.AddressId);
            Assert.Equal("5214 Madison Ave", returnedAddress.Address1);
        }
    }
}
