using AddressApi.Models;
using AddressApi.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace AddressApi.Tests
{
    public class AddressServiceTests
    {
        private readonly AddressService _service;
        private readonly Mock<ILogger<AddressService>> _mockLogger;

        public AddressServiceTests()
        {
            _mockLogger = new Mock<ILogger<AddressService>>();
            _service = new AddressService(_mockLogger.Object);
        }

        [Fact]
        public async Task GetAddresses_ReturnsAllAddresses()
        {
            // Act
            var result = await _service.GetAddressesAsync();

            // Assert
            Assert.NotNull(result);
            var addresses = result.ToList();
            Assert.Equal(4, addresses.Count);
        }

        [Fact]
        public async Task GetAddress_ReturnsCorrectAddress()
        {
            // Arrange
            int addressId = 1;

            // Act
            var result = await _service.GetAddressAsync(addressId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("5214 Madison Ave", result?.Address1);
            Assert.Equal("Chicago", result?.City);
        }

        [Fact]
        public async Task GetAddress_ReturnsNullForNonExistentAddress()
        {
            // Arrange
            int nonExistentAddressId = 99;

            // Act
            var result = await _service.GetAddressAsync(nonExistentAddressId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAddress_AddsAddressAndReturnsAddress()
        {
            // Arrange
            var newAddress = new Address
            {
                AddressId = 50,
                UserId = 3,
                Address1 = "789 New St",
                City = "New City",
                State = "NC",
                PostalCode = "12345",
                AddressType = AddressType.Billing
            };

            // Act
            var result = await _service.CreateAddressAsync(newAddress);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newAddress, result);
        }
    }
}
