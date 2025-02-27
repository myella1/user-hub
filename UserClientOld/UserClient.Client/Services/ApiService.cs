namespace UserClient.Client.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        public ApiService(HttpClient httpClient)
        { 
            _httpClient = httpClient;
        }

        public async Task<(IEnumerable<User> Users, IEnumerable<Address> Addresses)> GetDataAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<GatewayResponse>("api/management");
            return (response.Users, response.Addresses);
        }

        public async Task<User> AddUserAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/management/user", user);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<User>();
        }

        public async Task<Address> AddAddressAsync(Address address)
        {
            var response = await _httpClient.PostAsJsonAsync("api/management/address", address);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Address>();
        }
    }
    public class GatewayResponse
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
    }
}
