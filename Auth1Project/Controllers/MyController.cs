using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auth1Project.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MyController:ControllerBase
    {
        [HttpGet("public")]
        public IActionResult Public()
        {
            return Ok(new { message = "Public data" });
        }

        [HttpGet("private")]
        public IActionResult Private()
        {
            var userName = User.Identity.Name;
            return Ok(new { message = $"Hello {userName}" });
        }

        [HttpGet("admin")]
        [Authorize(Policy = "AdminOnly")] // فقط ادمین‌ها
        public IActionResult AdminOnly()
        {
            return Ok(new { message = "Admin secret" });
        }

        [HttpGet("user")]
        [Authorize(Policy = "UserOnly")] // فقط کاربران عادی
        public IActionResult UserOnly()
        {
            return Ok(new { message = "User area" });
        }

        [HttpGet("email-required")]
        [Authorize(Policy = "RequireEmail")] // کاربر باید Claim ایمیل داشته باشد
        public IActionResult EmailRequired()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            return Ok(new { message = $"Your email: {email}" });
        }
    }
}
