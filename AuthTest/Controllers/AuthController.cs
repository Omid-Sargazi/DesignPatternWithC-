using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IEmailService _emailService;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenService _refreshTokenService;
        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JwtSettings> jwtSettings,
            IEmailService emailService,
            ILogger<AuthController> logger,
            IConfiguration configuration,
            IRefreshTokenService refreshTokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _emailService = emailService;
            _logger = logger;
            _configuration = configuration;
            _refreshTokenService = refreshTokenService;
        }

        #region مدل‌های درخواست و پاسخ

        public class LoginRequest
        {
            [Required(ErrorMessage = "نام کاربری یا ایمیل الزامی است")]
            public string UsernameOrEmail { get; set; }

            [Required(ErrorMessage = "رمز عبور الزامی است")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            public bool RememberMe { get; set; }

            [Required(ErrorMessage = "کپچا الزامی است")]
            public string CaptchaToken { get; set; }
        }

        public class RegisterRequest
        {
            [Required(ErrorMessage = "نام الزامی است")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "نام خانوادگی الزامی است")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "نام کاربری الزامی است")]
            [MinLength(3, ErrorMessage = "نام کاربری باید حداقل ۳ کاراکتر باشد")]
            public string Username { get; set; }

            [Required(ErrorMessage = "ایمیل الزامی است")]
            [EmailAddress(ErrorMessage = "فرمت ایمیل نامعتبر است")]
            public string Email { get; set; }

            [Required(ErrorMessage = "رمز عبور الزامی است")]
            [MinLength(6, ErrorMessage = "رمز عبور باید حداقل ۶ کاراکتر باشد")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required(ErrorMessage = "تکرار رمز عبور الزامی است")]
            [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن مطابقت ندارند")]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; }

            [Phone(ErrorMessage = "فرمت تلفن نامعتبر است")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "کپچا الزامی است")]
            public string CaptchaToken { get; set; }
        }

        public class ForgotPasswordRequest
        {
            [Required(ErrorMessage = "ایمیل الزامی است")]
            [EmailAddress(ErrorMessage = "فرمت ایمیل نامعتبر است")]
            public string Email { get; set; }

            [Required(ErrorMessage = "کپچا الزامی است")]
            public string CaptchaToken { get; set; }
        }

        public class ResetPasswordRequest
        {
            [Required(ErrorMessage = "ایمیل الزامی است")]
            [EmailAddress(ErrorMessage = "فرمت ایمیل نامعتبر است")]
            public string Email { get; set; }

            [Required(ErrorMessage = "رمز عبور الزامی است")]
            [MinLength(6, ErrorMessage = "رمز عبور باید حداقل ۶ کاراکتر باشد")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required(ErrorMessage = "تکرار رمز عبور الزامی است")]
            [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن مطابقت ندارند")]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "توکن الزامی است")]
            public string Token { get; set; }
        }

        public class VerifyEmailRequest
        {
            [Required(ErrorMessage = "ایمیل الزامی است")]
            [EmailAddress(ErrorMessage = "فرمت ایمیل نامعتبر است")]
            public string Email { get; set; }

            [Required(ErrorMessage = "توکن الزامی است")]
            public string Token { get; set; }
        }

        public class RefreshTokenRequest
        {
            [Required(ErrorMessage = "توکن بازگردانی الزامی است")]
            public string RefreshToken { get; set; }

            [Required(ErrorMessage = "توکن دسترسی الزامی است")]
            public string AccessToken { get; set; }
        }

        public class LoginResponse
        {
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
            public DateTime ExpiresAt { get; set; }
            public UserProfile User { get; set; }
            public bool RequiresTwoFactor { get; set; }
            public IEnumerable<string> Roles { get; set; }
            public Dictionary<string, string> Claims { get; set; }
        }

        public class UserProfile
        {
            public string Id { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string FullName => $"{FirstName} {LastName}";
            public string PhoneNumber { get; set; }
            public string AvatarUrl { get; set; }
            public bool EmailConfirmed { get; set; }
            public bool PhoneNumberConfirmed { get; set; }
            public bool TwoFactorEnabled { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime? LastLogin { get; set; }
        }

        #endregion

        //#region لاگین پایه

        [HttpPost("login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //[RateLimit(10, 1)] // 10 درخواست در دقیقه
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            try
            {
                _logger.LogInformation("درخواست لاگین از {Username}", request.UsernameOrEmail);

                // 1. اعتبارسنجی کپچا
                if (!await ValidateCaptchaAsync(request.CaptchaToken))
                {
                    return BadRequest(new { Message = "کپچا نامعتبر است" });
                }

                // 2. پیدا کردن کاربر
                var user = await FindUserAsync(request.UsernameOrEmail);
                if (user == null)
                {
                    _logger.LogWarning("کاربر {Username} یافت نشد", request.UsernameOrEmail);
                    await Task.Delay(2000); // تاخیر برای جلوگیری از brute force
                    return Unauthorized(new { Message = "نام کاربری یا رمز عبور نادرست است" });
                }

                // 3. بررسی قفل بودن حساب
                if (await _userManager.IsLockedOutAsync(user))
                {
                    var lockoutEnd = await _userManager.GetLockoutEndDateAsync(user);
                    return Unauthorized(new
                    {
                        Message = "حساب کاربری قفل شده است",
                        LockoutEnd = lockoutEnd,
                        CanUnlock = lockoutEnd.HasValue && lockoutEnd.Value <= DateTime.UtcNow.AddMinutes(5)
                    });
                }

                // 4. بررسی نیاز به تایید ایمیل
                if (!user.EmailConfirmed && _configuration.GetValue<bool>("Auth:RequireEmailConfirmation"))
                {
                    return Unauthorized(new
                    {
                        Message = "لطفاً ابتدا ایمیل خود را تایید کنید",
                        RequiresEmailConfirmation = true,
                        Email = user.Email
                    });
                }

                // 5. بررسی رمز عبور
                var result = await _signInManager.PasswordSignInAsync(
                    user.UserName,
                    request.Password,
                    request.RememberMe,
                    lockoutOnFailure: true
                );

                if (!result.Succeeded)
                {
                    // افزایش شمارنده تلاش‌های ناموفق
                    await _userManager.AccessFailedAsync(user);

                    var attemptsLeft = _userManager.Options.Lockout.MaxFailedAccessAttempts -
                                     (await _userManager.GetAccessFailedCountAsync(user));

                    return Unauthorized(new
                    {
                        Message = "نام کاربری یا رمز عبور نادرست است",
                        AttemptsLeft = attemptsLeft,
                        IsLockedOut = result.IsLockedOut
                    });
                }

                // 6. بررسی نیاز به دو عاملی
                if (user.TwoFactorEnabled)
                {
                    // ارسال کد دو عاملی
                    var twoFactorToken = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
                    await _emailService.SendTwoFactorCodeAsync(user.Email, twoFactorToken);

                    _logger.LogInformation("کد دو عاملی برای {Email} ارسال شد", user.Email);

                    return Ok(new
                    {
                        RequiresTwoFactor = true,
                        Message = "کد تایید دو مرحله‌ای ارسال شد",
                        Provider = "Email",
                        UserId = user.Id
                    });
                }

                // 7. لاگ موفق - ایجاد توکن
                user.LastLogin = DateTime.UtcNow;
                await _userManager.UpdateAsync(user);

                // بازنشانی شمارنده خطاها
                await _userManager.ResetAccessFailedCountAsync(user);

                // 8. ایجاد توکن JWT
                var tokenResponse = await GenerateJwtTokenAsync(user);

                // 9. لاگ کردن لاگین موفق
                _logger.LogInformation("کاربر {UserId} با موفقیت وارد شد", user.Id);

                // 10. تنظیم کوکی‌های امن
                SetSecureCookies(tokenResponse);

                return Ok(tokenResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در فرآیند لاگین");
                return StatusCode(500, new { Message = "خطای داخلی سرور" });
            }
        }

        //#endregion

        #region ثبت نام

        [HttpPost("register")]
        [AllowAnonymous]
        //[RateLimit(5, 5)] // 5 درخواست در 5 دقیقه
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                // اعتبارسنجی کپچا
                if (!await ValidateCaptchaAsync(request.CaptchaToken))
                {
                    return BadRequest(new { Message = "کپچا نامعتبر است" });
                }

                // بررسی تکراری نبودن ایمیل
                if (await _userManager.FindByEmailAsync(request.Email) != null)
                {
                    return BadRequest(new { Message = "این ایمیل قبلاً ثبت شده است" });
                }

                // بررسی تکراری نبودن نام کاربری
                if (await _userManager.FindByNameAsync(request.Username) != null)
                {
                    return BadRequest(new { Message = "این نام کاربری قبلاً ثبت شده است" });
                }

                // ایجاد کاربر جدید
                var user = new ApplicationUser
                {
                    UserName = request.Username,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                    EmailConfirmed = !_configuration.GetValue<bool>("Auth:RequireEmailConfirmation"),
                    CreatedAt = DateTime.UtcNow,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description);
                    return BadRequest(new { Message = "خطا در ثبت نام", Errors = errors });
                }

                // افزودن نقش پیش‌فرض
                await _userManager.AddToRoleAsync(user, "User");

                // ارسال ایمیل تایید
                if (_configuration.GetValue<bool>("Auth:RequireEmailConfirmation"))
                {
                    var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action(
                        nameof(ConfirmEmail),
                        "Auth",
                        new { userId = user.Id, token = emailToken },
                        Request.Scheme
                    );

                    await _emailService.SendConfirmationEmailAsync(user.Email, confirmationLink, user.FullName);
                }

                // ارسال ایمیل خوش‌آمدگویی
                await _emailService.SendWelcomeEmailAsync(user.Email, user.FullName, user.UserName);

                _logger.LogInformation("کاربر جدید ثبت نام کرد: {Email}", user.Email);

                return Ok(new
                {
                    Message = "ثبت نام با موفقیت انجام شد",
                    UserId = user.Id,
                    RequiresEmailConfirmation = _configuration.GetValue<bool>("Auth:RequireEmailConfirmation")
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در ثبت نام");
                return StatusCode(500, new { Message = "خطای داخلی سرور" });
            }
        }

        #endregion

        #region تایید ایمیل

        [HttpGet("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return BadRequest(new { Message = "پارامترهای نامعتبر" });
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { Message = "کاربر یافت نشد" });
            }

            if (user.EmailConfirmed)
            {
                return Ok(new { Message = "ایمیل قبلاً تایید شده است" });
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return BadRequest(new { Message = "توکن نامعتبر است" });
            }

            _logger.LogInformation("ایمیل کاربر {Email} تایید شد", user.Email);

            // ریدایرکت به صفحه موفقیت
            return Redirect($"{_configuration["Frontend:Url"]}/email-confirmed");
        }

        [HttpPost("resend-confirmation")]
        [AllowAnonymous]
        public async Task<IActionResult> ResendConfirmation([FromBody] ForgotPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                // برای جلوگیری از User Enumeration همیشه OK برگردان
                return Ok(new { Message = "اگر ایمیل ثبت شده باشد، لینک تایید ارسال خواهد شد" });
            }

            if (user.EmailConfirmed)
            {
                return BadRequest(new { Message = "ایمیل قبلاً تایید شده است" });
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(
                nameof(ConfirmEmail),
                "Auth",
                new { userId = user.Id, token = token },
                Request.Scheme
            );

            await _emailService.SendConfirmationEmailAsync(user.Email, confirmationLink, user.FullName);

            return Ok(new { Message = "لینک تایید ایمیل مجدداً ارسال شد" });
        }

        #endregion

        #region بازیابی رمز عبور

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        //[RateLimit(3, 10)] // 3 درخواست در 10 دقیقه
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            // اعتبارسنجی کپچا
            if (!await ValidateCaptchaAsync(request.CaptchaToken))
            {
                return BadRequest(new { Message = "کپچا نامعتبر است" });
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // برای جلوگیری از User Enumeration
                return Ok(new { Message = "اگر ایمیل ثبت شده باشد، لینک بازیابی ارسال خواهد شد" });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // کد کوتاه برای SMS
            var shortCode = GenerateShortCode();

            // ذخیره کد در کش
            var cacheKey = $"ResetCode_{user.Id}";
            //await _distributedCache.SetStringAsync(cacheKey, shortCode,
            //    new DistributedCacheEntryOptions
            //    {
            //        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            //    });

            // ارسال ایمیل
            var resetLink = $"{_configuration["Frontend:Url"]}/reset-password?token={token}&email={user.Email}";
            await _emailService.SendPasswordResetEmailAsync(user.Email, resetLink, shortCode, user.FullName);

            // ارسال SMS اگر شماره تلفن وجود دارد
            if (!string.IsNullOrEmpty(user.PhoneNumber))
            {
                //await _smsService.SendPasswordResetCodeAsync(user.PhoneNumber, shortCode);
            }

            _logger.LogInformation("لینک بازیابی رمز عبور برای {Email} ارسال شد", user.Email);

            return Ok(new
            {
                Message = "لینک بازیابی رمز عبور ارسال شد",
                HasPhone = !string.IsNullOrEmpty(user.PhoneNumber)
            });
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest(new { Message = "کاربر یافت نشد" });
            }

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { Message = "خطا در بازنشانی رمز عبور", Errors = errors });
            }

            // لاگ کردن تغییر رمز
            _logger.LogInformation("رمز عبور کاربر {Email} تغییر کرد", user.Email);

            // ارسال ایمیل اطلاع‌رسانی
            await _emailService.SendPasswordChangedNotificationAsync(user.Email, user.FullName);

            return Ok(new { Message = "رمز عبور با موفقیت تغییر یافت" });
        }

        #endregion

        #region تایید دو مرحله‌ای

        [HttpPost("verify-2fa")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> VerifyTwoFactor(
            [FromQuery] string userId,
            [FromBody] TwoFactorVerificationRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Unauthorized(new { Message = "کاربر یافت نشد" });
            }

            var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", request.Code);
            if (!isValid)
            {
                return Unauthorized(new { Message = "کد تایید نامعتبر است" });
            }

            // ایجاد توکن پس از تایید دو مرحله‌ای
            var tokenResponse = await GenerateJwtTokenAsync(user);

            _logger.LogInformation("تایید دو مرحله‌ای برای کاربر {UserId} موفق بود", user.Id);

            return Ok(tokenResponse);
        }

        #endregion

        #region توکن‌ها

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var principal = GetPrincipalFromExpiredToken(request.AccessToken);
                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { Message = "توکن نامعتبر" });
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return Unauthorized(new { Message = "کاربر یافت نشد" });
                }

                // بررسی اعتبار Refresh Token
                var storedRefreshToken = await _refreshTokenService.GetRefreshTokenAsync(user.Id, request.RefreshToken);
                if (storedRefreshToken == null || storedRefreshToken.IsRevoked ||
                    storedRefreshToken.ExpiresAt < DateTime.UtcNow)
                {
                    return Unauthorized(new { Message = "توکن بازگردانی نامعتبر" });
                }

                // ابطال توکن قدیمی
                storedRefreshToken.RevokedAt = DateTime.UtcNow;
                await _refreshTokenService.UpdateRefreshTokenAsync(storedRefreshToken);

                // ایجاد توکن جدید
                var newTokenResponse = await GenerateJwtTokenAsync(user);

                return Ok(newTokenResponse);
            }
            catch (SecurityTokenException ex)
            {
                _logger.LogWarning(ex, "توکن JWT نامعتبر");
                return Unauthorized(new { Message = "توکن نامعتبر" });
            }
        }

        [HttpPost("revoke-token")]
        [Authorize]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(request.RefreshToken))
            {
                await _refreshTokenService.RevokeRefreshTokenAsync(userId, request.RefreshToken);
            }
            else
            {
                // ابطال همه توکن‌های کاربر
                await _refreshTokenService.RevokeAllUserTokensAsync(userId);
            }

            _logger.LogInformation("توکن‌های کاربر {UserId} ابطال شدند", userId);

            return Ok(new { Message = "توکن‌ها با موفقیت ابطال شدند" });
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // ابطال همه توکن‌های کاربر
            await _refreshTokenService.RevokeAllUserTokensAsync(userId);

            // حذف کوکی‌ها
            Response.Cookies.Delete("access_token");
            Response.Cookies.Delete("refresh_token");

            // لاگ کردن خروج
            _logger.LogInformation("کاربر {UserId} از سیستم خارج شد", userId);

            return Ok(new { Message = "با موفقیت خارج شدید" });
        }

        #endregion

        #region پروفایل کاربر

        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<UserProfile>> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { Message = "کاربر یافت نشد" });
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new UserProfile
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                CreatedAt = user.CreatedAt,
                LastLogin = user.LastLogin,
                AvatarUrl = user.AvatarUrl,
                Roles = roles
            });
        }

        [HttpPut("profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { Message = "کاربر یافت نشد" });
            }

            // به‌روزرسانی اطلاعات
            user.FirstName = request.FirstName ?? user.FirstName;
            user.LastName = request.LastName ?? user.LastName;
            user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(new { Message = "خطا در به‌روزرسانی پروفایل", Errors = result.Errors });
            }

            _logger.LogInformation("پروفایل کاربر {UserId} به‌روزرسانی شد", userId);

            return Ok(new { Message = "پروفایل با موفقیت به‌روزرسانی شد" });
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { Message = "کاربر یافت نشد" });
            }

            // بررسی رمز عبور فعلی
            var isCurrentPasswordValid = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
            if (!isCurrentPasswordValid)
            {
                return BadRequest(new { Message = "رمز عبور فعلی نادرست است" });
            }

            // تغییر رمز عبور
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(new { Message = "خطا در تغییر رمز عبور", Errors = result.Errors });
            }

            // ارسال ایمیل اطلاع‌رسانی
            await _emailService.SendPasswordChangedNotificationAsync(user.Email, user.FullName);

            _logger.LogInformation("رمز عبور کاربر {UserId} تغییر کرد", userId);

            return Ok(new { Message = "رمز عبور با موفقیت تغییر یافت" });
        }

        #endregion

        #region متدهای کمکی

        private async Task<ApplicationUser> FindUserAsync(string usernameOrEmail)
        {
            var user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(usernameOrEmail);
            }
            return user;
        }

        private async Task<LoginResponse> GenerateJwtTokenAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("firstName", user.FirstName ?? ""),
                new Claim("lastName", user.LastName ?? ""),
                new Claim("fullName", user.FullName),
                new Claim("avatar", user.AvatarUrl ?? ""),
                new Claim("emailConfirmed", user.EmailConfirmed.ToString()),
                new Claim("twoFactorEnabled", user.TwoFactorEnabled.ToString())
            };

            // افزودن نقش‌ها
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // افزودن Claimهای سفارشی
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            // ایجاد توکن دسترسی
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpiration),
                signingCredentials: creds
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            // ایجاد توکن بازگردانی
            var refreshToken = GenerateRefreshToken();

            // ذخیره توکن بازگردانی
            await _refreshTokenService.SaveRefreshTokenAsync(new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
                CreatedAt = DateTime.UtcNow,
                CreatedByIp = GetClientIpAddress()
            });

            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = token.ValidTo,
                User = new UserProfile
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    EmailConfirmed = user.EmailConfirmed,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    CreatedAt = user.CreatedAt,
                    LastLogin = user.LastLogin,
                    AvatarUrl = user.AvatarUrl
                },
                Roles = roles,
                Claims = userClaims.ToDictionary(c => c.Type, c => c.Value)
            };
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private string GenerateShortCode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                ValidateIssuer = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtSettings.Audience,
                ValidateLifetime = false // مهم: زمان انقضا را بررسی نکن
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("توکن نامعتبر");
            }

            return principal;
        }

        private void SetSecureCookies(LoginResponse tokens)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("access_token", tokens.AccessToken, cookieOptions);
            Response.Cookies.Append("refresh_token", tokens.RefreshToken, cookieOptions);
        }

        private string GetClientIpAddress()
        {
            return Request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        }

        private async Task<bool> ValidateCaptchaAsync(string captchaToken)
        {
            if (!_configuration.GetValue<bool>("Captcha:Enabled"))
                return true;

            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync("https://www.google.com/recaptcha/api/siteverify", new
            {
                secret = _configuration["Captcha:SecretKey"],
                response = captchaToken
            });

            var result = await response.Content.ReadFromJsonAsync<CaptchaResponse>();
            return result?.Success == true && result.Score >= 0.5;
        }

        private class CaptchaResponse
        {
            public bool Success { get; set; }
            public double Score { get; set; }
        }

        #endregion
    }
}