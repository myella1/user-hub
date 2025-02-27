using FrontEndApi.Models;
using FrontEndApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatewayController : ControllerBase
    {
        private readonly IGatewayService _gatewayService;
        public GatewayController(IGatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }

        [HttpGet]

        public async Task<IActionResult> GetData()
        {
            // Call backend APIs asynchronously in parallel
            var usersTask = _gatewayService.GetUsersAsync();
            var addressesTask = _gatewayService.GetAddressesAsync();

            await Task.WhenAll(usersTask, addressesTask);

            return Ok(new
            {
                Users = usersTask.Result,
                Addresses = addressesTask.Result
            });
        }

        [HttpPost("user")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var createdUser = await _gatewayService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetData), new { id = createdUser?.UserId }, createdUser);
        }

        [HttpPost("address")]
        public async Task<IActionResult> CreateAddress([FromBody] Address address)
        {
            var createdAddress = await _gatewayService.CreateAddressAsync(address);
            return CreatedAtAction(nameof(GetData), new { id = createdAddress?.UserId }, createdAddress);
        }
    }
}
