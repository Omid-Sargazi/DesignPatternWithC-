using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Auth1Project.Controllers
{
    public class AuthController
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {

        }
    }

    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
