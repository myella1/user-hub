using AddressApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AddressApi.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> GetAddressesAsync();
        Task<Address?> GetAddressAsync(int addressId);
        Task<Address> CreateAddressAsync([FromBody] Address address);
    }
}
