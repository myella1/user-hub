using UserGatewayApi.Services;
using UserGatewayApi.Models;
using Moq;
using Moq.Protected;
using System.Net.Http.Json;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UserGatewayApi.Options;

namespace UserGatewayApi.Tests
{
    public class GatewayServiceTests
    {
        private readonly Mock<ILogger<GatewayService>> _logger;
        private readonly Mock<IOptions<GatewayServiceOptions>> _options;
        private readonly HttpClient _httpClient;
        private readonly Mock<HttpMessageHandler> _httpMessageHandler;
        private readonly GatewayService _gatewayService;

        private const string UserApiUrl = "https://localhost/users";
        private const string AddressApiUrl = "https://localhost/addresses";

        public GatewayServiceTests()
        {
            _logger = new Mock<ILogger<GatewayService>>();
            _options = new Mock<IOptions<GatewayServiceOptions>>();

            _options.Setup(opt => opt.Value).Returns(new GatewayServiceOptions
            {
                UserApiUrl = UserApiUrl,
                AddressApiUrl = AddressApiUrl
            });

            _httpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://localhost")
            };

            _gatewayService = new GatewayService(_httpClient, _logger.Object, _options.Object);
        }

        [Fact]
        public async Task GetUsersAsync_ShouldReturnUsers_WhenApiReturnsSuccess()
        {
            var users = new List<User>{
                                            new() { UserId = 1, FirstName = "Mike", LastName = "Smith", EmailAddress = "MikeSmith@gmail.com" },
                                            new() { UserId = 2, FirstName = "Sarah", LastName = "Jane", EmailAddress = "SarahJane@gmail.com" }
                                      };
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(users)
            };

            _httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            var result = await _gatewayService.GetUsersAsync();

            Assert.NotNull(result);
            Assert.Equal(1, result.First().UserId);
        }

        [Fact]
        public async Task GetAddressesAsync_ShouldReturnAddresses_WhenApiReturnsSuccess()
        {
            // Arrange
            var addresses = new List<Address>
                                            {
                                                new() { AddressId = 1, UserId = 1, Address1 = "123 Main St", City = "Chicago", State = "IL", PostalCode = "12345", AddressType = AddressType.Shipping },
                                                new() { AddressId = 2, UserId = 2, Address1 = "456 Park Ave", City = "New York", State = "NY", PostalCode = "67890", AddressType = AddressType.Billing }
                                            };
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(addresses)
            };

            _httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            var result = await _gatewayService.GetAddressesAsync();

            Assert.NotNull(result);
            Assert.Equal(1, result.First().AddressId);
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
            var responseMessage = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = JsonContent.Create(newUser)
            };

            _httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            var result = await _gatewayService.CreateUserAsync(newUser);

            Assert.NotNull(result);

            Assert.Equal(newUser.UserId, result.UserId);
            Assert.Equal("John", result.FirstName);
        }

        [Fact]
        public async Task CreateAddressAsync_ShouldReturnAddress_WhenApiReturnsSuccess()
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
            var responseMessage = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = JsonContent.Create(newAddress)
            };

            _httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            var result = await _gatewayService.CreateAddressAsync(newAddress);

            Assert.NotNull(result);
            Assert.Equal(newAddress.AddressId, result.AddressId);
            Assert.Equal("Boston", result.City);
        }
    }
}
