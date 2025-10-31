using Microsoft.AspNetCore.Mvc;
using UserContactRegistration.Domain.Models;
using UserContactRegistration.Domain.Services;

namespace UserContactRegistration.Application.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistration request)
        {
            return Ok(await _userService.RegisterUser(request));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdate request)
        {
            var response = await _userService.UpdateUser(request);
            return Ok(response);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var response = await _userService.DeleteUser(id);
            return Ok(response);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetUserById(long id)
        {
            var response = await _userService.GetUserById(id);
            return Ok(response);
        }

    }
}
