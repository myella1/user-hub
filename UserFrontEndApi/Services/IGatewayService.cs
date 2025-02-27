using FrontEndApi.Models;

namespace FrontEndApi.Services
{
    public interface IGatewayService
    {
        Task<IEnumerable<User>?> GetUsersAsync();
        Task<IEnumerable<Address>?> GetAddressesAsync();
        Task<User?> CreateUserAsync(User user);
        Task<Address?> CreateAddressAsync(Address address);
    }
}
