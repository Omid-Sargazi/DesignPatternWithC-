using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SMSAuthentication.Models;

namespace SMSAuthentication.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMemoryCache _cache;
        private readonly ILogger<AuthController> _logger;

        public AuthController(UserManager<ApplicationUser> userManager, IMemoryCache cache, ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _cache = cache;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingUser = await _userManager.FindByNameAsync(model.MobileNumber);
            if (existingUser != null)
            {
                return BadRequest("این شماره موبایل قبلاً ثبت شده است.");
            }

            string verificationCode = new Random().Next(100000, 999999).ToString();

            _logger.LogInformation($"Verification code for {model.MobileNumber}: {verificationCode}");

            _cache.Set(model.MobileNumber, verificationCode,TimeSpan.FromMinutes(1));

            return Ok("Sent a confirm code");
        }


        [HttpPost("verify")]
        public async Task<IActionResult> Verify([FromBody] VerifyModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Retrieve code from cache
            if (!_cache.TryGetValue(model.MobileNumber, out string storedCode))
            {
                return BadRequest("کد تأیید منقضی شده یا وجود ندارد.");
            }

            if (model.VerificationCode != storedCode)
            {
                return BadRequest("کد تأیید نادرست است.");
            }

            // Create user without password
            var user = new ApplicationUser
            {
                UserName = model.MobileNumber, // Use MobileNumber as UserName for uniqueness
                PhoneNumber = model.MobileNumber,
                Message = model.Message,
                PasswordHash = null // No password
            };

            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Remove code from cache
            _cache.Remove(model.MobileNumber);

            return Ok("کاربر با موفقیت ثبت شد.");
        }


    }
}
