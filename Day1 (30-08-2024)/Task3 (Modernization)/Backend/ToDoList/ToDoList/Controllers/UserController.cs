using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyCors")]

    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userService;

        public UserController(IUserRepository userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user data.");
            }

            var result = await _userService.RegisterUserAsync(user);

            if (result ==1)
            {
                return Ok(new { message = "User registered successfully!" });
            }

            return BadRequest("User registration failed.");
        }

        [HttpGet("register")]
        public IActionResult GetRegisterPage()
        {
            // Return a view or a message indicating the registration page
            return Content("Registration page should be displayed here.");
        }
    }
}
