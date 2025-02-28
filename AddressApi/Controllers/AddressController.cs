using AddressApi.Models;
using AddressApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AddressApi.Controllers
{
    /// <summary>
    /// Address Controller - Used to retrieve and create Address resource.
    /// </summary>
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

        /// <summary>
        ///Retrieves Addresses using the "Get" pattern.
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<Address>?> GetAddresses()
        {
            try
            {
                await Task.Delay(5000);
                return await _addressService.GetAddresses();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        /// <summary>
        ///Retrieves specific address using the "Get" pattern.
        /// </summary>
        [HttpGet("{addressId}")]
        public async Task<Address?> GetAddress(int addressId)
        {
            try
            {
                await Task.Delay(5000);
                return await _addressService.GetAddress(addressId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        /// <summary>
        ///Creates Address resource using the "POST" pattern.
        /// </summary>
        [HttpPost]
        public async Task<Address?> CreateAddress([FromBody] Address address)
        {
            try
            {
                await Task.Delay(10000);
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
