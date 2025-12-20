using API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace API.Services
{
    public interface ITwoFactorService
    {
        Task<string> GenerateCodeAsync(string userId);
        Task<bool> ValidateCodeAsync(string userId, string code);
        Task<bool> IsEnabledAsync(string userId);
        Task<bool> EnableAsync(string userId, string code);
        Task<bool> DisableAsync(string userId, string code);
        Task<IEnumerable<RecoveryCode>> GenerateRecoveryCodesAsync(string userId, int count = 10);
        Task<bool> ValidateRecoveryCodeAsync(string userId, string recoveryCode);
        Task<bool> SendCodeAsync(string userId, string method = "Email");
    }

    public class RecoveryCode
    {
        public string Code { get; set; }
        public bool IsUsed { get; set; }
        public DateTime GeneratedAt { get; set; }
        public DateTime? UsedAt { get; set; }
    }

    public class TwoFactorService : ITwoFactorService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<TwoFactorService> _logger;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public TwoFactorService(
            UserManager<ApplicationUser> userManager,
            ILogger<TwoFactorService> logger,
            IEmailService emailService,
            ISmsService smsService,
            ApplicationDbContext context,
            IMemoryCache cache)
        {
            _userManager = userManager;
            _logger = logger;
            _emailService = emailService;
            _smsService = smsService;
            _context = context;
            _cache = cache;
        }

        public async Task<string> GenerateCodeAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    throw new ArgumentException("User not found", nameof(userId));
                }

                // تولید کد ۶ رقمی
                var random = new Random();
                var code = random.Next(100000, 999999).ToString();

                // ذخیره در کش با مدت اعتبار ۵ دقیقه
                var cacheKey = $"2FA_{userId}";
                _cache.Set(cacheKey, new TwoFactorCode
                {
                    Code = code,
                    GeneratedAt = DateTime.UtcNow,
                    UserId = userId,
                    Attempts = 0
                }, TimeSpan.FromMinutes(5));

                _logger.LogDebug("2FA code generated for user {UserId}", userId);

                return code;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating 2FA code for user {UserId}", userId);
                throw;
            }
        }

        public async Task<bool> ValidateCodeAsync(string userId, string code)
        {
            try
            {
                var cacheKey = $"2FA_{userId}";
                if (!_cache.TryGetValue<TwoFactorCode>(cacheKey, out var twoFactorCode))
                {
                    _logger.LogWarning("No active 2FA code found for user {UserId}", userId);
                    return false;
                }

                // بررسی تعداد تلاش‌ها
                if (twoFactorCode.Attempts >= 3)
                {
                    _logger.LogWarning("Too many 2FA attempts for user {UserId}", userId);
                    _cache.Remove(cacheKey);
                    return false;
                }

                // بررسی انقضا
                if (twoFactorCode.GeneratedAt < DateTime.UtcNow.AddMinutes(-5))
                {
                    _logger.LogWarning("Expired 2FA code for user {UserId}", userId);
                    _cache.Remove(cacheKey);
                    return false;
                }

                // بررسی تطابق کد
                if (twoFactorCode.Code != code)
                {
                    twoFactorCode.Attempts++;
                    _cache.Set(cacheKey, twoFactorCode, TimeSpan.FromMinutes(5));

                    _logger.LogWarning("Invalid 2FA code for user {UserId}. Attempt {Attempt}",
                        userId, twoFactorCode.Attempts);
                    return false;
                }

                // کد معتبر
                _cache.Remove(cacheKey);

                // ثبت لاگ موفق
                await RecordValidationSuccessAsync(userId);

                _logger.LogInformation("Valid 2FA code for user {UserId}", userId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating 2FA code for user {UserId}", userId);
                return false;
            }
        }

        public async Task<bool> IsEnabledAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                return user?.TwoFactorEnabled ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking 2FA status for user {UserId}", userId);
                return false;
            }
        }

        public async Task<bool> EnableAsync(string userId, string code)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return false;
                }

                // بررسی کد
                if (!await ValidateCodeAsync(userId, code))
                {
                    return false;
                }

                // فعال‌سازی 2FA
                var result = await _userManager.SetTwoFactorEnabledAsync(user, true);
                if (result.Succeeded)
                {
                    user.TwoFactorEnabled = true;
                    await _userManager.UpdateAsync(user);

                    // تولید کدهای بازیابی
                    await GenerateRecoveryCodesAsync(userId);

                    _logger.LogInformation("2FA enabled for user {UserId}", userId);
                    return true;
                }

                _logger.LogWarning("Failed to enable 2FA for user {UserId}", userId);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error enabling 2FA for user {UserId}", userId);
                return false;
            }
        }

        public async Task<bool> DisableAsync(string userId, string code)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return false;
                }

                // اگر کد ارسال شده کد بازیابی است
                if (code.Length == 10) // فرض: کدهای بازیابی ۱۰ کاراکتری هستند
                {
                    if (!await ValidateRecoveryCodeAsync(userId, code))
                    {
                        return false;
                    }
                }
                else // کد عادی 2FA
                {
                    if (!await ValidateCodeAsync(userId, code))
                    {
                        return false;
                    }
                }

                // غیرفعال‌سازی 2FA
                var result = await _userManager.SetTwoFactorEnabledAsync(user, false);
                if (result.Succeeded)
                {
                    user.TwoFactorEnabled = false;
                    await _userManager.UpdateAsync(user);

                    // حذف کدهای بازیابی
                    await RemoveRecoveryCodesAsync(userId);

                    _logger.LogInformation("2FA disabled for user {UserId}", userId);
                    return true;
                }

                _logger.LogWarning("Failed to disable 2FA for user {UserId}", userId);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error disabling 2FA for user {UserId}", userId);
                return false;
            }
        }

        public async Task<IEnumerable<RecoveryCode>> GenerateRecoveryCodesAsync(string userId, int count = 10)
        {
            try
            {
                var codes = new List<RecoveryCode>();
                var random = new Random();

                for (int i = 0; i < count; i++)
                {
                    var code = GenerateRecoveryCode(random);
                    codes.Add(new RecoveryCode
                    {
                        Code = code,
                        IsUsed = false,
                        GeneratedAt = DateTime.UtcNow
                    });
                }

                // ذخیره در دیتابیس
                var recoveryCodes = codes.Select(c => new TwoFactorRecoveryCode
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    Code = c.Code,
                    IsUsed = false,
                    GeneratedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddYears(1)
                }).ToList();

                await _context.TwoFactorRecoveryCodes.AddRangeAsync(recoveryCodes);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Generated {Count} recovery codes for user {UserId}", count, userId);

                return codes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating recovery codes for user {UserId}", userId);
                return Enumerable.Empty<RecoveryCode>();
            }
        }

        public async Task<bool> ValidateRecoveryCodeAsync(string userId, string recoveryCode)
        {
            try
            {
                var code = await _context.TwoFactorRecoveryCodes
                    .FirstOrDefaultAsync(c =>
                        c.UserId == userId &&
                        c.Code == recoveryCode &&
                        !c.IsUsed &&
                        c.ExpiresAt > DateTime.UtcNow);

                if (code == null)
                {
                    _logger.LogWarning("Invalid or expired recovery code for user {UserId}", userId);
                    return false;
                }

                // علامت‌گذاری به عنوان استفاده شده
                code.IsUsed = true;
                code.UsedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                _logger.LogInformation("Valid recovery code used for user {UserId}", userId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating recovery code for user {UserId}", userId);
                return false;
            }
        }

        public async Task<bool> SendCodeAsync(string userId, string method = "Email")
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return false;
                }

                var code = await GenerateCodeAsync(userId);

                switch (method.ToLower())
                {
                    case "email":
                        await _emailService.SendTwoFactorCodeAsync(user.Email, code);
                        break;

                    case "sms":
                        if (!string.IsNullOrEmpty(user.PhoneNumber))
                        {
                            await _smsService.SendTwoFactorCodeAsync(user.PhoneNumber, code);
                        }
                        else
                        {
                            _logger.LogWarning("No phone number for user {UserId} to send SMS", userId);
                            return false;
                        }
                        break;

                    case "authenticator":
                        // برای اپلیکیشن‌های Authenticator
                        // کد قبلاً تولید شده است
                        break;

                    default:
                        _logger.LogWarning("Unknown 2FA method: {Method}", method);
                        return false;
                }

                _logger.LogInformation("2FA code sent via {Method} to user {UserId}", method, userId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending 2FA code to user {UserId}", userId);
                return false;
            }
        }

        private async Task RecordValidationSuccessAsync(string userId)
        {
            try
            {
                var history = new TwoFactorValidationHistory
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    ValidatedAt = DateTime.UtcNow,
                    IpAddress = GetClientIpAddress(),
                    IsSuccessful = true
                };

                await _context.TwoFactorValidationHistories.AddAsync(history);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error recording 2FA validation history");
            }
        }

        private async Task RemoveRecoveryCodesAsync(string userId)
        {
            try
            {
                var codes = await _context.TwoFactorRecoveryCodes
                    .Where(c => c.UserId == userId)
                    .ToListAsync();

                if (codes.Any())
                {
                    _context.TwoFactorRecoveryCodes.RemoveRange(codes);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing recovery codes for user {UserId}", userId);
            }
        }

        private string GenerateRecoveryCode(Random random)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string GetClientIpAddress()
        {
            try
            {
                var httpContextAccessor = new HttpContextAccessor();
                return httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            }
            catch
            {
                return "Unknown";
            }
        }
    }

    public class TwoFactorCode
    {
        public string Code { get; set; }
        public DateTime GeneratedAt { get; set; }
        public string UserId { get; set; }
        public int Attempts { get; set; }
    }

    public class TwoFactorRecoveryCode
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Code { get; set; }
        public bool IsUsed { get; set; }
        public DateTime GeneratedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime? UsedAt { get; set; }
    }

    public class TwoFactorValidationHistory
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime ValidatedAt { get; set; }
        public string IpAddress { get; set; }
        public bool IsSuccessful { get; set; }
    }
}