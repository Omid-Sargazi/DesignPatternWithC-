using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth1Project.Controllers
{
    [Authorize] // همه Actions این کنترلر نیاز به لاگین دارند
    [ApiController]
    [Route("api/[controller]")]
    public class SecureController : ControllerBase
    {
        [HttpGet("data")]
        public IActionResult GetData()
        {
            return Ok(new { data = "Protected data" });
        }

        [AllowAnonymous] // این Action استثنا است
        [HttpGet("public")]
        public IActionResult Public()
        {
            return Ok(new { data = "Public data" });
        }
    }
}
