using UserGatewayApi.Services;
using UserGatewayApi.Models;
using Moq;
using Moq.Protected;
using System.Net.Http.Json;
using System.Net;

namespace UserGatewayApi.Tests
{
    public class GatewayServiceTests
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly HttpClient _httpClient;
        private readonly GatewayService _service;

        private const string UserApiUrl = "https://localhost:7280/api/User";
        private const string AddressApiUrl = "https://localhost:7219/api/Address";

        public GatewayServiceTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://localhost/")
            };
            _service = new GatewayService(_httpClient);
        }

        [Fact]
        public async Task GetUsersAsync_ReturnsUsers()
        {
            // Arrange
            var users = new List<User>{
                                            new() { UserId = 1, FirstName = "Mike", LastName = "Smith", EmailAddress = "MikeSmith@gmail.com" },
                                            new() { UserId = 2, FirstName = "Sarah", LastName = "Jane", EmailAddress = "SarahJane@gmail.com" }
                                      };

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(users)
            };

            _ = _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                                                    "SendAsync",
                                                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString() == UserApiUrl),
                                                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);
            // Act
            var result = await _service.GetUsersAsync();

            // Assert
            Assert.NotNull(result);
            var userList = result.ToList();
            Assert.Equal(2, userList.Count);
            Assert.Equal("Mike", userList[0].FirstName);
        }

        [Fact]
        public async Task GetAddressesAsync_ReturnsAddressList()
        {
            // Arrange
            var addresses = new List<Address>
                                            {
                                                new() { AddressId = 1, UserId = 1, Address1 = "123 Main St", City = "Chicago", State = "IL", PostalCode = "12345", AddressType = AddressType.Shipping },
                                                new() { AddressId = 2, UserId = 2, Address1 = "456 Park Ave", City = "New York", State = "NY", PostalCode = "67890", AddressType = AddressType.Billing }
                                            };


            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(addresses)
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                                                "SendAsync",
                                                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString() == AddressApiUrl),
                                                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            // Act
            var result = await _service.GetAddressesAsync();

            // Assert
            Assert.NotNull(result);
            var addressList = result.ToList();
            Assert.Equal(2, addressList.Count);
            Assert.Equal("Chicago", addressList[0].City);
        }

        [Fact]
        public async Task CreateUserAsync_CreatesAndReturnsUser()
        {
            // Arrange
            var newUser = new User
            {
                UserId = 3,
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "JohnDoe@gmail.com"
            };
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Created,
                Content = JsonContent.Create(newUser)
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                                                "SendAsync",
                                                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString() == UserApiUrl),
                                                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            // Act
            var result = await _service.CreateUserAsync(newUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newUser.UserId, result.UserId);
            Assert.Equal("John", result.FirstName);
        }

        [Fact]
        public async Task CreateAddressAsync_CreatesAndReturnsAddress()
        {
            // Arrange
            var newAddress = new Address
            {
                AddressId = 3,
                UserId = 3,
                Address1 = "789 Elm St",
                City = "Boston",
                State = "MA",
                PostalCode = "54321",
                AddressType = AddressType.Billing
            };
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Created,
                Content = JsonContent.Create(newAddress)
            };

            _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                                            "SendAsync",
                                            ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString() == AddressApiUrl),
                                            ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

            // Act
            var result = await _service.CreateAddressAsync(newAddress);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newAddress.AddressId, result.AddressId);
            Assert.Equal("Boston", result.City);
        }
    }
}
