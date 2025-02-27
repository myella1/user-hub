using FrontEndApi.Models;

namespace FrontEndApi.Services
{
    public class GatewayService : IGatewayService
    {
        private readonly HttpClient _httpClient;        
        // URLs can be set in configuration (e.g., appsettings.json) and injected
        private readonly string _userApiUrl = "https://localhost:7280/api/User";
        private readonly string _addressApiUrl = "https://localhost:7219/api/Address";

        public GatewayService(HttpClient httpClient)
        {
            _httpClient = httpClient;            
        }
        public async Task<IEnumerable<User>?> GetUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<User>>(_userApiUrl);
        }
        public async Task<IEnumerable<Address>?> GetAddressesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Address>>(_addressApiUrl);
        }
        public async Task<User?> CreateUserAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync(_userApiUrl, user);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<User>();
        }        
        public async Task<Address?> CreateAddressAsync(Address address)
        {
            var response = await _httpClient.PostAsJsonAsync(_addressApiUrl, address);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Address>();
        }
    }
}
