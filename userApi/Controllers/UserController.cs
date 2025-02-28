using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Services;

namespace UserApi.Controllers
{
    /// <summary>
    /// User Controller - Used to retrieve and create user resource.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        ///Retrieves all the users using the "Get" pattern.
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<User>?> GetUsers()
        {
            try
            {
                await Task.Delay(500);
                return await _userService.GetUsers();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        /// <summary>
        ///Retrieves specific user by the user id using the "Get" pattern.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<User?> GetUser(int id)
        {
            try
            {
                await Task.Delay(5000);
                return await _userService.GetUser(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        /// <summary>
        ///Creates user resource using the "POST" pattern.
        /// </summary>
        [HttpPost]
        public async Task<User?> CreateUser([FromBody] User user)
        {
            try
            {
                await Task.Delay(15000);
                return await _userService.CreateUser(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
