using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using UserApiClient.Models;

namespace UserApiClient.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient; 
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,  // Ensure property names are case-insensitive
            Converters = { new JsonStringEnumConverter() } // Convert enums from strings
        };
        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<(IEnumerable<User>? Users, IEnumerable<Address>? Addresses)> GetDataAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<GatewayResponse>("api/gateway", _jsonOptions);
            return (response?.Users, response?.Addresses);
        }

        public async Task<User?> AddUserAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/gateway/user", user);
            response.EnsureSuccessStatusCode();
            var userResponse = await response.Content.ReadFromJsonAsync<User>(_jsonOptions);
            return userResponse;
        }

        public async Task<Address?> AddAddressAsync(Address address)
        {
            var response = await _httpClient.PostAsJsonAsync("api/gateway/address", address);
            response.EnsureSuccessStatusCode();
            var addressResponse = await response.Content.ReadFromJsonAsync<Address>(_jsonOptions);
            return addressResponse;
        }
    }

    public class GatewayResponse
    {
        public IEnumerable<User>? Users { get; set; }
        public IEnumerable<Address>? Addresses { get; set; }
    }
}
