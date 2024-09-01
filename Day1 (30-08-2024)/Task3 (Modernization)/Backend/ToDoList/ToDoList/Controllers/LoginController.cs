using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyCors")]

    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _loginService;

        public LoginController(ILoginRepository loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginModel)
        {
            if (loginModel == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid login request.");
            }

            bool isValidUser = await _loginService.ValidateAsync(loginModel);

            if (isValidUser)
            {
                // Generate token or session
                return Ok(new { message = "Login successful" });
            }
            else
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }
        }
    }
}
