using AddressApi.Models;
using AddressApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AddressApi.Controllers
{
    /// <summary>
    /// Address Controller - Used to get and create Address resource.
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
        ///Retrieves Address resources using the "Get" pattern.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAddresses()
        {
            try
            {
                var addresses = await _addressService.GetAddressesAsync();
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving addresses.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving addresses.");
            }
        }

        /// <summary>
        ///Retrieves specific address resource using the "Get" pattern.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult?> GetAddress(int id)
        {
            try
            {
                var address = await _addressService.GetAddressAsync(id);
                if (address == null)
                {
                    _logger.LogWarning($"Address with id {id} was not found.");
                    return NotFound();
                }
                return Ok(address);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving address with id {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the address.");
            }
        }

        /// <summary>
        ///Creates Address resource using the "POST" pattern.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] Address address)
        {
            try
            {
                var createdAddress = await _addressService.CreateAddressAsync(address);
                return Ok(address);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating address.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating address.");
            }        
        }
    }
}
