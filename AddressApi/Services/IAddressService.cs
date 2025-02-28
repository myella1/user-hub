using AddressApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AddressApi.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> GetAddresses();
        Task<Address?> GetAddress(int addressId);
        Task<Address> CreateAddress([FromBody] Address address);
    }
}
