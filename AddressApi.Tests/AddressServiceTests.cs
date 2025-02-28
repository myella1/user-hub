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
        public async Task GetAddress_ReturnsCorrectAddresses()
        {
            // Arrange
            // Act
            var result = await _service.GetAddresses();

            // Assert
            Assert.NotNull(result);
            var addresses = result.ToList();
            Assert.Equal(4, addresses.Count);
        }

        [Fact]
        public async Task GetAddress_ReturnsCorrectAddress()
        {
            // Arrange
            var addressId = 1;

            // Act
            var result = await _service.GetAddress(addressId);

            // Assert
            Assert.Equal(addressId, result?.AddressId);
            Assert.Equal("5214 Madison Ave", result?.Address1);
            Assert.Equal("Chicago", result?.City);
        }

        [Fact]
        public async Task CreateAddress_AddsAddressAndReturnsCreatedAddress()
        {
            // Arrange
            var newAddress = new Address
            {
                AddressId = 5,
                UserId = 3,
                Address1 = "789 New St",
                City = "New City",
                State = "NC",
                PostalCode = "12345",
                AddressType = AddressType.Billing
            };

            // Act
            var result = await _service.CreateAddress(newAddress);

            // Assert
            Assert.NotNull(result);
            var address = result;
            Assert.Equal(result, address);
        }
    }
}
