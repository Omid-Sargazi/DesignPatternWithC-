using API.Data;
using Microsoft.EntityFrameworkCore;
using UAParser;

namespace API.Services
{
    public interface ILoginHistoryService
    {
        Task RecordLoginSuccessAsync(string userId, string ipAddress, string userAgent);
        Task RecordLoginFailureAsync(string userId, string ipAddress, string userAgent, string reason);
        Task<IEnumerable<LoginHistory>> GetUserLoginHistoryAsync(string userId, int days = 30);
        Task<IEnumerable<LoginHistory>> GetRecentLoginsAsync(int count = 100);
        Task<int> GetFailedAttemptsCountAsync(string userId, TimeSpan timeWindow);
        Task ClearOldHistoryAsync(int daysToKeep = 90);
        Task<LoginStatistics> GetLoginStatisticsAsync(string userId);
        Task<IEnumerable<SuspiciousActivity>> DetectSuspiciousActivitiesAsync(string userId);
    }

    public class LoginHistoryService : ILoginHistoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LoginHistoryService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGeoLocationService _geoLocationService;

        public LoginHistoryService(
            ApplicationDbContext context,
            ILogger<LoginHistoryService> logger,
            IHttpContextAccessor httpContextAccessor,
            IGeoLocationService geoLocationService)
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _geoLocationService = geoLocationService;
        }

        public async Task RecordLoginSuccessAsync(string userId, string ipAddress, string userAgent)
        {
            try
            {
                var deviceInfo = ParseUserAgent(userAgent);
                var location = await _geoLocationService.GetLocationAsync(ipAddress);

                var loginHistory = new LoginHistory
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    IpAddress = ipAddress,
                    UserAgent = userAgent,
                    DeviceType = deviceInfo.DeviceType,
                    Browser = deviceInfo.Browser,
                    Platform = deviceInfo.Platform,
                    Country = location.Country,
                    City = location.City,
                    LoginTime = DateTime.UtcNow,
                    IsSuccessful = true,
                    FailureReason = null
                };

                _context.LoginHistories.Add(loginHistory);
                await _context.SaveChangesAsync();

                _logger.LogInformation("لاگین موفق برای کاربر {UserId} از {IpAddress} ثبت شد", userId, ipAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در ثبت تاریخچه لاگین موفق برای کاربر {UserId}", userId);
            }
        }

        public async Task RecordLoginFailureAsync(string userId, string ipAddress, string userAgent, string reason)
        {
            try
            {
                var deviceInfo = ParseUserAgent(userAgent);
                var location = await _geoLocationService.GetLocationAsync(ipAddress);

                var loginHistory = new LoginHistory
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    IpAddress = ipAddress,
                    UserAgent = userAgent,
                    DeviceType = deviceInfo.DeviceType,
                    Browser = deviceInfo.Browser,
                    Platform = deviceInfo.Platform,
                    Country = location.Country,
                    City = location.City,
                    LoginTime = DateTime.UtcNow,
                    IsSuccessful = false,
                    FailureReason = reason
                };

                _context.LoginHistories.Add(loginHistory);
                await _context.SaveChangesAsync();

                _logger.LogWarning("لاگین ناموفق برای کاربر {UserId} از {IpAddress}: {Reason}", userId, ipAddress, reason);

                // بررسی فعالیت مشکوک
                await CheckForSuspiciousActivityAsync(userId, ipAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در ثبت تاریخچه لاگین ناموفق برای کاربر {UserId}", userId);
            }
        }

        public async Task<IEnumerable<LoginHistory>> GetUserLoginHistoryAsync(string userId, int days = 30)
        {
            try
            {
                var fromDate = DateTime.UtcNow.AddDays(-days);

                return await _context.LoginHistories
                    .Where(lh => lh.UserId == userId && lh.LoginTime >= fromDate)
                    .OrderByDescending(lh => lh.LoginTime)
                    .Take(100)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در دریافت تاریخچه لاگین کاربر {UserId}", userId);
                return Enumerable.Empty<LoginHistory>();
            }
        }

        public async Task<ICollection<LoginHistory>> GetRecentLoginsAsync(int count = 100)
        {
            try
            {
                return _context.LoginHistories
                    .OrderByDescending(lh => lh.LoginTime)
                    .Take(count);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در دریافت آخرین لاگین‌ها");
                return Enumerable.Empty<LoginHistory>();
            }
        }

        public async Task<int> GetFailedAttemptsCountAsync(string userId, TimeSpan timeWindow)
        {
            try
            {
                var fromTime = DateTime.UtcNow.Subtract(timeWindow);

                return await _context.LoginHistories
                    .CountAsync(lh =>
                        lh.UserId == userId &&
                        !lh.IsSuccessful &&
                        lh.LoginTime >= fromTime);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در شمارش تلاش‌های ناموفق برای کاربر {UserId}", userId);
                return 0;
            }
        }

        public async Task ClearOldHistoryAsync(int daysToKeep = 90)
        {
            try
            {
                var cutoffDate = DateTime.UtcNow.AddDays(-daysToKeep);

                var oldHistories = await _context.LoginHistories
                    .Where(lh => lh.LoginTime < cutoffDate)
                    .ToListAsync();

                if (oldHistories.Any())
                {
                    _context.LoginHistories.RemoveRange(oldHistories);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("{Count} رکورد تاریخچه قدیمی حذف شد", oldHistories.Count);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در پاک‌سازی تاریخچه قدیمی");
            }
        }

        public async Task<LoginStatistics> GetLoginStatisticsAsync(string userId)
        {
            try
            {
                var last30Days = DateTime.UtcNow.AddDays(-30);

                var histories = await _context.LoginHistories
                    .Where(lh => lh.UserId == userId && lh.LoginTime >= last30Days)
                    .ToListAsync();

                return new LoginStatistics
                {
                    TotalLogins = histories.Count,
                    SuccessfulLogins = histories.Count(lh => lh.IsSuccessful),
                    FailedLogins = histories.Count(lh => !lh.IsSuccessful),
                    UniqueIps = histories.Select(lh => lh.IpAddress).Distinct().Count(),
                    UniqueDevices = histories.Select(lh => lh.DeviceType).Distinct().Count(),
                    LastLogin = histories.OrderByDescending(lh => lh.LoginTime).FirstOrDefault(),
                    MostCommonIp = histories
                        .GroupBy(lh => lh.IpAddress)
                        .OrderByDescending(g => g.Count())
                        .Select(g => g.Key)
                        .FirstOrDefault(),
                    MostCommonDevice = histories
                        .GroupBy(lh => lh.DeviceType)
                        .OrderByDescending(g => g.Count())
                        .Select(g => g.Key)
                        .FirstOrDefault()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در محاسبه آمار لاگین کاربر {UserId}", userId);
                return new LoginStatistics();
            }
        }

        public async Task<IEnumerable<SuspiciousActivity>> DetectSuspiciousActivitiesAsync(string userId)
        {
            try
            {
                var suspiciousActivities = new List<SuspiciousActivity>();
                var last24Hours = DateTime.UtcNow.AddHours(-24);

                var recentLogins = await _context.LoginHistories
                    .Where(lh => lh.UserId == userId && lh.LoginTime >= last24Hours)
                    .OrderByDescending(lh => lh.LoginTime)
                    .ToListAsync();

                if (!recentLogins.Any())
                    return suspiciousActivities;

                // 1. شناسایی لاگین از IPهای مختلف در زمان کوتاه
                var uniqueIps = recentLogins.Select(lh => lh.IpAddress).Distinct().ToList();
                if (uniqueIps.Count > 3)
                {
                    suspiciousActivities.Add(new SuspiciousActivity
                    {
                        Type = "Multiple IPs",
                        Description = $"لاگین از {uniqueIps.Count} آی‌پی مختلف در ۲۴ ساعت گذشته",
                        Severity = "Medium",
                        DetectedAt = DateTime.UtcNow,
                        RelatedIps = uniqueIps
                    });
                }

                // 2. شناسایی تلاش‌های ناموفق متوالی
                var failedLogins = recentLogins.Where(lh => !lh.IsSuccessful).ToList();
                if (failedLogins.Count >= 5)
                {
                    suspiciousActivities.Add(new SuspiciousActivity
                    {
                        Type = "Brute Force Attempt",
                        Description = $"{failedLogins.Count} تلاش ناموفق در ۲۴ ساعت گذشته",
                        Severity = "High",
                        DetectedAt = DateTime.UtcNow,
                        RelatedIps = failedLogins.Select(lh => lh.IpAddress).Distinct().ToList()
                    });
                }

                // 3. شناسایی لاگین از مکان‌های جغرافیایی دور
                var locations = recentLogins
                    .Where(lh => !string.IsNullOrEmpty(lh.Country))
                    .Select(lh => lh.Country)
                    .Distinct()
                    .ToList();

                if (locations.Count > 2)
                {
                    suspiciousActivities.Add(new SuspiciousActivity
                    {
                        Type = "Geographic Anomaly",
                        Description = $"لاگین از {locations.Count} کشور مختلف: {string.Join(", ", locations)}",
                        Severity = "High",
                        DetectedAt = DateTime.UtcNow,
                        RelatedCountries = locations
                    });
                }

                // 4. شناسایی لاگین در ساعت غیرمعمول
                var unusualHourLogins = recentLogins
                    .Where(lh => lh.LoginTime.Hour >= 0 && lh.LoginTime.Hour <= 5)
                    .ToList();

                if (unusualHourLogins.Any() && unusualHourLogins.Count > recentLogins.Count * 0.5)
                {
                    suspiciousActivities.Add(new SuspiciousActivity
                    {
                        Type = "Unusual Login Time",
                        Description = $"{unusualHourLogins.Count} لاگین در ساعت غیرمعمول (بین ۱۲ تا ۵ صبح)",
                        Severity = "Low",
                        DetectedAt = DateTime.UtcNow
                    });
                }

                return suspiciousActivities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در شناسایی فعالیت‌های مشکوک برای کاربر {UserId}", userId);
                return Enumerable.Empty<SuspiciousActivity>();
            }
        }

        private async Task CheckForSuspiciousActivityAsync(string userId, string ipAddress)
        {
            try
            {
                var lastHour = DateTime.UtcNow.AddHours(-1);

                var failedAttempts = await _context.LoginHistories
                    .CountAsync(lh =>
                        lh.UserId == userId &&
                        !lh.IsSuccessful &&
                        lh.IpAddress == ipAddress &&
                        lh.LoginTime >= lastHour);

                if (failedAttempts >= 3)
                {
                    _logger.LogWarning(
                        "فعالیت مشکوک شناسایی شد: {Attempts} تلاش ناموفق از آی‌پی {IpAddress} برای کاربر {UserId}",
                        failedAttempts, ipAddress, userId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در بررسی فعالیت مشکوک");
            }
        }

        private (string DeviceType, string Browser, string Platform) ParseUserAgent(string userAgent)
        {
            try
            {
                var uaParser = Parser.GetDefault();
                var clientInfo = uaParser.Parse(userAgent);

                var deviceType = clientInfo.Device.Family;
                if (deviceType == "Other")
                {
                    deviceType = userAgent.Contains("Mobile") ? "Mobile" :
                                userAgent.Contains("Tablet") ? "Tablet" : "Desktop";
                }

                return (
                    DeviceType: deviceType,
                    Browser: $"{clientInfo.UA.Family} {clientInfo.UA.Major}",
                    Platform: $"{clientInfo.OS.Family} {clientInfo.OS.Major}"
                );
            }
            catch
            {
                return ("Unknown", "Unknown", "Unknown");
            }
        }
    }

    public class LoginStatistics
    {
        public int TotalLogins { get; set; }
        public int SuccessfulLogins { get; set; }
        public int FailedLogins { get; set; }
        public int UniqueIps { get; set; }
        public int UniqueDevices { get; set; }
        public LoginHistory LastLogin { get; set; }
        public string MostCommonIp { get; set; }
        public string MostCommonDevice { get; set; }
        public double SuccessRate => TotalLogins > 0 ? (SuccessfulLogins * 100.0) / TotalLogins : 0;
    }

    public class SuspiciousActivity
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public string Severity { get; set; }
        public DateTime DetectedAt { get; set; }
        public List<string> RelatedIps { get; set; } = new();
        public List<string> RelatedCountries { get; set; } = new();
    }
}