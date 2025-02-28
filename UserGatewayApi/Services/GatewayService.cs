using UserGatewayApi.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Options;
using UserGatewayApi.Options;

namespace UserGatewayApi.Services
{
    public class GatewayService : IGatewayService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GatewayService> _logger;
        private readonly string _userApiUrl;
        private readonly string _addressApiUrl; 
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public GatewayService(HttpClient httpClient, ILogger<GatewayService> logger, IOptions<GatewayServiceOptions> options)
        {
            _httpClient = httpClient;
            _logger = logger;

            var config = options.Value;
            _userApiUrl = config.UserApiUrl;
            _addressApiUrl = config.AddressApiUrl;
        }

        public async Task<IEnumerable<User>?> GetUsersAsync()
        {
            try
            {
                _logger.LogInformation("Fetching users from {UserApiUrl}", _userApiUrl);
                var users = await _httpClient.GetFromJsonAsync<IEnumerable<User>>(_userApiUrl);
                _logger.LogInformation($"Successfully fetched {users?.Count()??0} users.");
                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching users from {_userApiUrl}.");
                throw;
            }
        }

        public async Task<IEnumerable<Address>?> GetAddressesAsync()
        {
            try
            {
                _logger.LogInformation($"Fetching addresses from {_addressApiUrl}");
                var addresses = await _httpClient.GetFromJsonAsync<IEnumerable<Address>>(_addressApiUrl, _jsonOptions);
                _logger.LogInformation($"Successfully fetched {addresses?.Count() ?? 0} addresses.");
                return addresses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching addresses from {_addressApiUrl}.");
                throw;
            }
        }

        public async Task<User?> CreateUserAsync(User user)
        {
            try
            {
                _logger.LogInformation("Creating a new user.");
                var response = await _httpClient.PostAsJsonAsync(_userApiUrl, user);
                response.EnsureSuccessStatusCode();
                var userResponse = await response.Content.ReadFromJsonAsync<User>(_jsonOptions);
                _logger.LogInformation($"User created successfully with id {userResponse?.UserId}.");
                return userResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a user.");
                throw;
            }
        }

        public async Task<Address?> CreateAddressAsync(Address address)
        {
            try
            {
                _logger.LogInformation("Creating a new address.");
                var response = await _httpClient.PostAsJsonAsync(_addressApiUrl, address);
                response.EnsureSuccessStatusCode();
                var addressResponse = await response.Content.ReadFromJsonAsync<Address>(_jsonOptions);
                _logger.LogInformation($"Address created successfully with id {addressResponse?.AddressId}.");
                return addressResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating an address.");
                throw;
            }
        }
    }
}
