using EmployeesManagement.API.Interfaces;
using EmployeesManagement.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody]UserCredentials userParam)
        {
            var token = _userService.Authenticate(userParam.Username, userParam.Password);

            if (token == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
                
            var authResponse = new AuthResponse
            {
                Token = token,
                Username = userParam.Username
            };
            return Ok(authResponse);
        }

        public IActionResult Get()
        {
            return Ok("You are authenticated");
        }

    }
}