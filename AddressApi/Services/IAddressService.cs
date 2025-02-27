using AddressApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AddressApi.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> GetAddress();
        Task<Address> CreateAddress([FromBody] Address address);
    }
}
