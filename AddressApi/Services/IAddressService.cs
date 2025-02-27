using AddressApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AddressApi.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> GetAddress(int userId);
        Task<IEnumerable<Address>> CreateAddress([FromBody] Address address);
    }
}
