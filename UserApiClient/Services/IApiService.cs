using UserApiClient.Models;

namespace UserApiClient.Services
{
    public interface IApiService
    {
        Task<(IEnumerable<User>? Users, IEnumerable<Address>? Addresses)> GetDataAsync();
        Task<User?> AddUserAsync(User user);
        Task<Address?> AddAddressAsync(Address address);
    }
}
