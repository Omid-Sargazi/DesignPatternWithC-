using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using API.Data;

namespace API.Services
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken> GetRefreshTokenAsync(string userId, string token);
        Task SaveRefreshTokenAsync(RefreshToken refreshToken);
        Task UpdateRefreshTokenAsync(RefreshToken refreshToken);
        Task RevokeRefreshTokenAsync(string userId, string token);
        Task RevokeAllUserTokensAsync(string userId);
        Task CleanupExpiredTokensAsync();
        Task<IEnumerable<RefreshToken>> GetUserTokensAsync(string userId);
        Task<bool> IsTokenValidAsync(string userId, string token);
        Task RotateRefreshTokenAsync(string oldToken, RefreshToken newToken);
    }

    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly ApplicationDbContext _context;
        private readonly IDistributedCache _cache;
        private readonly ILogger<RefreshTokenService> _logger;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);

        public RefreshTokenService(
            ApplicationDbContext context,
            IDistributedCache cache,
            ILogger<RefreshTokenService> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(string userId, string token)
        {
            var cacheKey = $"RefreshToken_{userId}_{token}";

            try
            {
                // تلاش برای دریافت از کش
                var cachedToken = await _cache.GetStringAsync(cacheKey);
                if (!string.IsNullOrEmpty(cachedToken))
                {
                    return JsonSerializer.Deserialize<RefreshToken>(cachedToken);
                }

                // دریافت از دیتابیس
                var refreshToken = await _context.RefreshTokens
                    .Include(rt => rt.User)
                    .FirstOrDefaultAsync(rt =>
                        rt.UserId == userId &&
                        rt.Token == token &&
                        !rt.IsRevoked &&
                        !rt.IsExpired);

                if (refreshToken != null)
                {
                    // ذخیره در کش
                    await _cache.SetStringAsync(
                        cacheKey,
                        JsonSerializer.Serialize(refreshToken),
                        new DistributedCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = _cacheDuration
                        });
                }

                return refreshToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در دریافت توکن بازگردانی برای کاربر {UserId}", userId);
                return null;
            }
        }

        public async Task SaveRefreshTokenAsync(RefreshToken refreshToken)
        {
            try
            {
                _context.RefreshTokens.Add(refreshToken);
                await _context.SaveChangesAsync();

                // ذخیره در کش
                var cacheKey = $"RefreshToken_{refreshToken.UserId}_{refreshToken.Token}";
                await _cache.SetStringAsync(
                    cacheKey,
                    JsonSerializer.Serialize(refreshToken),
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = _cacheDuration
                    });

                _logger.LogDebug("توکن بازگردانی برای کاربر {UserId} ذخیره شد", refreshToken.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در ذخیره توکن بازگردانی برای کاربر {UserId}", refreshToken.UserId);
                throw;
            }
        }

        public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken)
        {
            try
            {
                _context.RefreshTokens.Update(refreshToken);
                await _context.SaveChangesAsync();

                // به‌روزرسانی کش
                var cacheKey = $"RefreshToken_{refreshToken.UserId}_{refreshToken.Token}";
                await _cache.SetStringAsync(
                    cacheKey,
                    JsonSerializer.Serialize(refreshToken),
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = _cacheDuration
                    });

                _logger.LogDebug("توکن بازگردانی برای کاربر {UserId} به‌روزرسانی شد", refreshToken.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در به‌روزرسانی توکن بازگردانی برای کاربر {UserId}", refreshToken.UserId);
                throw;
            }
        }

        public async Task RevokeRefreshTokenAsync(string userId, string token)
        {
            try
            {
                var refreshToken = await GetRefreshTokenAsync(userId, token);
                if (refreshToken == null)
                {
                    _logger.LogWarning("توکن بازگردانی برای ابطال یافت نشد: {UserId}, {Token}", userId, token);
                    return;
                }

                refreshToken.RevokedAt = DateTime.UtcNow;
                refreshToken.RevokedByIp = GetClientIpAddress();

                await UpdateRefreshTokenAsync(refreshToken);

                // حذف از کش
                var cacheKey = $"RefreshToken_{userId}_{token}";
                await _cache.RemoveAsync(cacheKey);

                _logger.LogInformation("توکن بازگردانی برای کاربر {UserId} ابطال شد", userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در ابطال توکن بازگردانی برای کاربر {UserId}", userId);
                throw;
            }
        }

        public async Task RevokeAllUserTokensAsync(string userId)
        {
            try
            {
                var tokens = await _context.RefreshTokens
                    .Where(rt => rt.UserId == userId && !rt.IsRevoked && !rt.IsExpired)
                    .ToListAsync();

                foreach (var token in tokens)
                {
                    token.RevokedAt = DateTime.UtcNow;
                    token.RevokedByIp = "System";

                    // حذف از کش
                    var cacheKey = $"RefreshToken_{userId}_{token.Token}";
                    await _cache.RemoveAsync(cacheKey);
                }

                if (tokens.Any())
                {
                    _context.RefreshTokens.UpdateRange(tokens);
                    await _context.SaveChangesAsync();
                }

                _logger.LogInformation("همه توکن‌های کاربر {UserId} ابطال شدند", userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در ابطال همه توکن‌های کاربر {UserId}", userId);
                throw;
            }
        }

        public async Task CleanupExpiredTokensAsync()
        {
            try
            {
                var expiredTokens = await _context.RefreshTokens
                    .Where(rt => rt.IsExpired || rt.IsRevoked)
                    .ToListAsync();

                if (expiredTokens.Any())
                {
                    _context.RefreshTokens.RemoveRange(expiredTokens);
                    await _context.SaveChangesAsync();

                    // حذف از کش
                    foreach (var token in expiredTokens)
                    {
                        var cacheKey = $"RefreshToken_{token.UserId}_{token.Token}";
                        await _cache.RemoveAsync(cacheKey);
                    }

                    _logger.LogInformation("{Count} توکن منقضی حذف شدند", expiredTokens.Count);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در پاک‌سازی توکن‌های منقضی");
            }
        }

        public async Task<IEnumerable<RefreshToken>> GetUserTokensAsync(string userId)
        {
            try
            {
                var cacheKey = $"UserTokens_{userId}";

                var cachedTokens = await _cache.GetStringAsync(cacheKey);
                if (!string.IsNullOrEmpty(cachedTokens))
                {
                    return JsonSerializer.Deserialize<List<RefreshToken>>(cachedTokens);
                }

                var tokens = await _context.RefreshTokens
                    .Where(rt => rt.UserId == userId)
                    .OrderByDescending(rt => rt.CreatedAt)
                    .Take(10)
                    .ToListAsync();

                // ذخیره در کش
                await _cache.SetStringAsync(
                    cacheKey,
                    JsonSerializer.Serialize(tokens),
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                    });

                return tokens;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در دریافت توکن‌های کاربر {UserId}", userId);
                return Enumerable.Empty<RefreshToken>();
            }
        }

        public async Task<bool> IsTokenValidAsync(string userId, string token)
        {
            try
            {
                var refreshToken = await GetRefreshTokenAsync(userId, token);
                return refreshToken != null && refreshToken.IsActive;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در بررسی اعتبار توکن برای کاربر {UserId}", userId);
                return false;
            }
        }

        public async Task RotateRefreshTokenAsync(string oldToken, RefreshToken newToken)
        {
            try
            {
                // پیدا کردن توکن قدیمی
                var oldRefreshToken = await _context.RefreshTokens
                    .FirstOrDefaultAsync(rt => rt.Token == oldToken && rt.IsActive);

                if (oldRefreshToken != null)
                {
                    // ابطال توکن قدیمی
                    oldRefreshToken.RevokedAt = DateTime.UtcNow;
                    oldRefreshToken.RevokedByIp = newToken.CreatedByIp;
                    oldRefreshToken.ReplacedByToken = newToken.Token;

                    await UpdateRefreshTokenAsync(oldRefreshToken);
                }

                // ذخیره توکن جدید
                await SaveRefreshTokenAsync(newToken);

                _logger.LogDebug("توکن بازگردانی برای کاربر {UserId} چرخش یافت", newToken.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در چرخش توکن بازگردانی");
                throw;
            }
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
}