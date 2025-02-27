using AddressApi.Models;
using AddressApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AddressApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ILogger<AddressController> _logger;
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService, ILogger<AddressController> logger)
        {
            _addressService = addressService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Address>?> GetAddress()
        {
            try
            {
                await Task.Delay(500);
                return await _addressService.GetAddress();
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        [HttpPost]
        public async Task<Address?> CreateAddress([FromBody] Address address)
        {
            try
            {
                await Task.Delay(1000);
                return await _addressService.CreateAddress(address);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }            
        }
    }
}
