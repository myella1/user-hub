using UserGatewayApi.Models;
using UserGatewayApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace UserGatewayApi.Controllers
{
    /// <summary>
    /// Gateway Api Controller - Used to invoke UserApi and AddressApi to retrieve Data.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GatewayController : ControllerBase
    {
        private readonly IGatewayService _gatewayService;
        private readonly ILogger<GatewayController> _logger;
        public GatewayController(IGatewayService gatewayService, ILogger<GatewayController> logger)
        {
            _gatewayService = gatewayService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            try
            {
                _logger.LogInformation("Invoking gateway service to retrieve users and addresses.");

                // Call backend APIs asynchronously in parallel
                var usersTask = _gatewayService.GetUsersAsync();
                var addressesTask = _gatewayService.GetAddressesAsync();

                await Task.WhenAll(usersTask, addressesTask);

                _logger.LogInformation("Successfully retrieved gateway data.");
                return Ok(new
                {
                    Users = await usersTask,
                    Addresses = await addressesTask
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving gateway data.");
                return StatusCode(500, "An error occurred while retrieving data.");
            }
        }

        [HttpPost("user")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            try
            {
                _logger.LogInformation("Creating a new user.");
                var createdUser = await _gatewayService.CreateUserAsync(user);
                _logger.LogInformation("User created with Id {UserId}.", createdUser?.UserId);
                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new user.");
                return StatusCode(500, "An error occurred while creating the user.");
            }
        }

        [HttpPost("address")]
        public async Task<IActionResult> CreateAddress([FromBody] Address address)
        {
            try
            {
                _logger.LogInformation("Creating a new address.");
                var createdAddress = await _gatewayService.CreateAddressAsync(address);
                _logger.LogInformation("Address created with Id {AddressId}.", createdAddress?.AddressId);
                return Ok(createdAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new address.");
                return StatusCode(500, "An error occurred while creating the address.");
            }
        }
    }
}
