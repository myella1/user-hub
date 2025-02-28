using UserGatewayApi.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace UserGatewayApi.Services
{
    public class GatewayService : IGatewayService
    {
        private readonly HttpClient _httpClient;        
        // URLs can be set in configuration (e.g., appsettings.json) and injected
        private readonly string _userApiUrl = "https://localhost:7280/api/User";
        private readonly string _addressApiUrl = "https://localhost:7219/api/Address";

        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,  // Ensure property names are case-insensitive
            Converters = { new JsonStringEnumConverter() } // Convert enums from strings
        };
        public GatewayService(HttpClient httpClient)
        {
            _httpClient = httpClient;            
        }
        public async Task<IEnumerable<User>?> GetUsersAsync()
        {
            var users = await _httpClient.GetFromJsonAsync<IEnumerable<User>>(_userApiUrl);
            return users;
        }
        public async Task<IEnumerable<Address>?> GetAddressesAsync()
        {
            var addresses = await _httpClient.GetFromJsonAsync<IEnumerable<Address>>(_addressApiUrl, _jsonOptions);
            return addresses;
        }
        public async Task<User?> CreateUserAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync(_userApiUrl, user);
            response.EnsureSuccessStatusCode();
            var userResponse = await response.Content.ReadFromJsonAsync<User>(_jsonOptions);
            return userResponse;
        }        
        public async Task<Address?> CreateAddressAsync(Address address)
        {
            var response = await _httpClient.PostAsJsonAsync(_addressApiUrl, address);
            response.EnsureSuccessStatusCode(); 
            var addressResponse = await response.Content.ReadFromJsonAsync<Address>(_jsonOptions);
            return addressResponse;
        }
    }
}
