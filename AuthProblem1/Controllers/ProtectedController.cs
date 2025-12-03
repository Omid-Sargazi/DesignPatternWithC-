using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthProblem1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // نیاز به توکن JWT دارد
    public class ProtectedController : ControllerBase
    {
        [HttpGet("data")]
        public IActionResult GetData()
        {
            var userName = User.Identity.Name;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new
            {
                message = "This is protected data!",
                user = new
                {
                    id = userId,
                    name = userName,
                    role = userRole
                },
                timestamp = DateTime.UtcNow
            });
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")] // فقط ادمین‌ها
        public IActionResult AdminOnly()
        {
            return Ok(new { message = "Welcome Admin!" });
        }
    }
}
