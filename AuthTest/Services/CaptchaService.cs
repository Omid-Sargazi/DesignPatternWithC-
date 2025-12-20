using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace API.Services
{
    public interface ICaptchaService
    {
        Task<bool> ValidateAsync(string token, string userIp = null);
        Task<CaptchaConfig> GetConfigAsync();
        Task<CaptchaScore> GetScoreAsync(string token);
        Task<bool> IsHumanAsync(string token, double threshold = 0.5);
    }

    public class CaptchaConfig
    {
        public string SiteKey { get; set; }
        public bool Enabled { get; set; }
        public double Threshold { get; set; } = 0.5;
        public bool UseInvisibleCaptcha { get; set; }
        public string Theme { get; set; } = "light";
        public string Size { get; set; } = "normal";
    }

    public class CaptchaScore
    {
        public bool Success { get; set; }
        public double Score { get; set; }
        public DateTime ChallengeTimestamp { get; set; }
        public string Hostname { get; set; }
        public List<string> ErrorCodes { get; set; }
        public string Action { get; set; }
        public bool IsHuman => Score >= 0.5;
        public bool IsBot => Score < 0.3;
        public string RiskLevel => Score switch
        {
            >= 0.7 => "Low",
            >= 0.5 => "Medium",
            >= 0.3 => "High",
            _ => "Very High"
        };
    }

    public class CaptchaService : ICaptchaService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CaptchaService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;

        public CaptchaService(
            HttpClient httpClient,
            ILogger<CaptchaService> logger,
            IConfiguration configuration,
            IMemoryCache cache)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
            _cache = cache;
        }

        public async Task<bool> ValidateAsync(string token, string userIp = null)
        {
            try
            {
                // اگر کپچا غیرفعال است
                if (!_configuration.GetValue<bool>("Captcha:Enabled"))
                {
                    _logger.LogDebug("Captcha validation is disabled");
                    return true;
                }

                // بررسی کش برای جلوگیری از درخواست‌های تکراری
                var cacheKey = $"Captcha_{token}";
                if (_cache.TryGetValue<bool>(cacheKey, out var cachedResult))
                {
                    return cachedResult;
                }

                var secretKey = _configuration["Captcha:SecretKey"];
                if (string.IsNullOrEmpty(secretKey))
                {
                    _logger.LogWarning("Captcha secret key is not configured");
                    return true;
                }

                // ارسال درخواست به Google reCAPTCHA
                var formData = new Dictionary<string, string>
                {
                    ["secret"] = secretKey,
                    ["response"] = token
                };

                if (!string.IsNullOrEmpty(userIp))
                {
                    formData["remoteip"] = userIp;
                }

                var content = new FormUrlEncodedContent(formData);
                var response = await _httpClient.PostAsync(
                    "https://www.google.com/recaptcha/api/siteverify",
                    content);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Failed to validate captcha. Status: {StatusCode}", response.StatusCode);
                    return false;
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RecaptchaResponse>(responseBody);

                // لاگ کردن نتیجه برای آنالیز
                _logger.LogDebug("Captcha validation result - Success: {Success}, Score: {Score}, Hostname: { Hostname}", result.Success, result.Score, result.Hostname);

                // ذخیره در کش به مدت 5 دقیقه
                _cache.Set(cacheKey, result.Success, TimeSpan.FromMinutes(5));

                return result.Success && result.Score >= GetThreshold();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating captcha token");

                // در صورت خطا، در حالت توسعه اجازه بده
                if (_configuration.GetValue<bool>("Captcha:AllowOnError"))
                {
                    return true;
                }

                return false;
            }
        }

        public async Task<CaptchaConfig> GetConfigAsync()
        {
            return new CaptchaConfig
            {
                SiteKey = _configuration["Captcha:SiteKey"],
                Enabled = _configuration.GetValue<bool>("Captcha:Enabled"),
                Threshold = _configuration.GetValue<double>("Captcha:Threshold", 0.5),
                UseInvisibleCaptcha = _configuration.GetValue<bool>("Captcha:UseInvisible", false),
                Theme = _configuration["Captcha:Theme"] ?? "light",
                Size = _configuration["Captcha:Size"] ?? "normal"
            };
        }

        public async Task<CaptchaScore> GetScoreAsync(string token)
        {
            try
            {
                var secretKey = _configuration["Captcha:SecretKey"];
                if (string.IsNullOrEmpty(secretKey))
                {
                    return new CaptchaScore
                    {
                        Success = false,
                        ErrorCodes = new List<string> { "SECRET_KEY_NOT_CONFIGURED" }
                    };
                }

                var formData = new Dictionary<string, string>
                {
                    ["secret"] = secretKey,
                    ["response"] = token
                };

                var content = new FormUrlEncodedContent(formData);
                var response = await _httpClient.PostAsync(
                    "https://www.google.com/recaptcha/api/siteverify",
                    content);

                if (!response.IsSuccessStatusCode)
                {
                    return new CaptchaScore
                    {
                        Success = false,
                        ErrorCodes = new List<string> { $"HTTP_{response.StatusCode}" }
                    };
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RecaptchaResponse>(responseBody);

                return new CaptchaScore
                {
                    Success = result.Success,
                    Score = result.Score,
                    ChallengeTimestamp = UnixTimeStampToDateTime(result.ChallengeTs),
                    Hostname = result.Hostname,
                    ErrorCodes = result.ErrorCodes,
                    Action = result.Action
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting captcha score");
                return new CaptchaScore
                {
                    Success = false,
                    ErrorCodes = new List<string> { "EXCEPTION" }
                };
            }
        }

        public async Task<bool> IsHumanAsync(string token, double threshold = 0.5)
        {
            var score = await GetScoreAsync(token);
            return score.Success && score.Score >= threshold;
        }

        private double GetThreshold()
        {
            return _configuration.GetValue<double>("Captcha:Threshold", 0.5);
        }

        private DateTime UnixTimeStampToDateTime(string unixTimeStamp)
        {
            if (string.IsNullOrEmpty(unixTimeStamp))
                return DateTime.MinValue;

            if (DateTime.TryParse(unixTimeStamp, out var result))
                return result;

            return DateTime.MinValue;
        }
    }

    public class RecaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("score")]
        public double Score { get; set; }

        [JsonProperty("challenge_ts")]
        public string ChallengeTs { get; set; }

        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }
    }
}