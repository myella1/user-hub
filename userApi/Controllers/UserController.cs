using Microsoft.AspNetCore.Mvc;
using userApi.Models;
using UserApi.Services;

namespace userApi.Controllers
{
    /// <summary>
    /// User Controller
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
        public async Task<IEnumerable<User>> GetUsers()
        {
            await Task.Delay(500);
            return await _userService.GetUsers();
        }

        /// <summary>
        ///Retrieves specific user by the user id using the "Get" pattern.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<User> GetUser(int id)
        {
            await Task.Delay(1000);
            return await _userService.GetUser(id);
        }

        /// <summary>
        ///Creates or updates user resource using the "POST" pattern.
        /// </summary>
        [HttpPost]
        public async Task<IEnumerable<User>> CreateUser([FromBody] User user)
        {
            await Task.Delay(1000);
            return await _userService.CreateUser(user);
        }
    }
}
