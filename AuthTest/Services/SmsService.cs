using Microsoft.Extensions.Caching.Memory;
using System.Text;
using Newtonsoft.Json;

namespace API.Services
{
    public interface ISmsService
    {
        Task<bool> SendSmsAsync(string phoneNumber, string message);
        Task<bool> SendVerificationCodeAsync(string phoneNumber, string code);
        Task<bool> SendPasswordResetCodeAsync(string phoneNumber, string code);
        Task<bool> SendTwoFactorCodeAsync(string phoneNumber, string code);
        Task<bool> SendSecurityAlertAsync(string phoneNumber, string alertType);
        Task<bool> ValidatePhoneNumberAsync(string phoneNumber);
    }

    public class SmsService : ISmsService
    {
        private readonly ILogger<SmsService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _cache;

        public SmsService(
            ILogger<SmsService> logger,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IMemoryCache cache)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _cache = cache;
        }

        public async Task<bool> SendSmsAsync(string phoneNumber, string message)
        {
            try
            {
                if (!await ValidatePhoneNumberAsync(phoneNumber))
                {
                    _logger.LogWarning("Invalid phone number: {PhoneNumber}", phoneNumber);
                    return false;
                }

                // بررسی محدودیت ارسال
                var rateLimitKey = $"SmsRateLimit_{phoneNumber}";
                if (_cache.TryGetValue<int>(rateLimitKey, out var sentCount) && sentCount >= 5)
                {
                    _logger.LogWarning("Rate limit exceeded for phone number: {PhoneNumber}", phoneNumber);
                    return false;
                }

                var provider = _configuration["Sms:Provider"] ?? "Kavenegar";
                var formattedNumber = FormatPhoneNumber(phoneNumber);

                bool result = provider.ToLower() switch
                {
                    "kavenegar" => await SendViaKavenegarAsync(formattedNumber, message),
                    "melipayamak" => await SendViaMelipayamakAsync(formattedNumber, message),
                    "twilio" => await SendViaTwilioAsync(formattedNumber, message),
                    _ => await SendViaKavenegarAsync(formattedNumber, message)
                };

                if (result)
                {
                    // افزایش شمارنده محدودیت
                    var newCount = sentCount + 1;
                    _cache.Set(rateLimitKey, newCount, TimeSpan.FromHours(1));

                    _logger.LogInformation("SMS sent successfully to {PhoneNumber}", formattedNumber);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending SMS to {PhoneNumber}", phoneNumber);
                return false;
            }
        }

        public async Task<bool> SendVerificationCodeAsync(string phoneNumber, string code)
        {
            var message = $"کد تایید شما: {code}\n\nاین کد ۵ دقیقه اعتبار دارد.";
            var template = _configuration["Sms:Templates:Verification"] ?? "verify";

            return await SendSmsAsync(phoneNumber, message);
        }

        public async Task<bool> SendPasswordResetCodeAsync(string phoneNumber, string code)
        {
            var message = $"کد بازیابی رمز عبور: {code}\n\nاین کد ۱۰ دقیقه اعتبار دارد.";
            return await SendSmsAsync(phoneNumber, message);
        }

        public async Task<bool> SendTwoFactorCodeAsync(string phoneNumber, string code)
        {
            var message = $"کد تایید دو مرحله‌ای: {code}\n\nاین کد ۵ دقیقه اعتبار دارد.";
            return await SendSmsAsync(phoneNumber, message);
        }

        public async Task<bool> SendSecurityAlertAsync(string phoneNumber, string alertType)
        {
            var message = $"هشدار امنیتی: {alertType}\n\nدر صورت نیاز با پشتیبانی تماس بگیرید.";
            return await SendSmsAsync(phoneNumber, message);
        }

        public async Task<bool> ValidatePhoneNumberAsync(string phoneNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(phoneNumber))
                    return false;

                // حذف فاصله و کاراکترهای غیرعددی
                var cleaned = new string(phoneNumber.Where(char.IsDigit).ToArray());

                // بررسی طول شماره
                if (cleaned.Length < 10 || cleaned.Length > 15)
                    return false;

                // بررسی پیش‌شماره ایران
                if (cleaned.StartsWith("98") && cleaned.Length == 12)
                    return true;

                // شماره‌های با 0
                if (cleaned.StartsWith("09") && cleaned.Length == 11)
                    return true;

                // شماره‌های بین‌المللی
                if (cleaned.StartsWith("+") || cleaned.StartsWith("00"))
                {
                    var withoutPrefix = cleaned.StartsWith("+") ?
                        cleaned.Substring(1) : cleaned.Substring(2);

                    return withoutPrefix.All(char.IsDigit) &&
                           withoutPrefix.Length >= 10 &&
                           withoutPrefix.Length <= 13;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> SendViaKavenegarAsync(string phoneNumber, string message)
        {
            try
            {
                var apiKey = _configuration["Sms:Kavenegar:ApiKey"];
                var sender = _configuration["Sms:Kavenegar:Sender"] ?? "10004346";

                var httpClient = _httpClientFactory.CreateClient();
                var url = $"https://api.kavenegar.com/v1/{apiKey}/sms/send.json";

                var formData = new Dictionary<string, string>
                {
                    ["receptor"] = phoneNumber,
                    ["message"] = message,
                    ["sender"] = sender
                };

                var content = new FormUrlEncodedContent(formData);
                var response = await httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<KavenegarResponse>(responseBody);

                    return result?.Return?.Status == 200;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> SendViaMelipayamakAsync(string phoneNumber, string message)
        {
            try
            {
                var username = _configuration["Sms:Melipayamak:Username"];
                var password = _configuration["Sms:Melipayamak:Password"];
                var from = _configuration["Sms:Melipayamak:From"] ?? "5000";

                var httpClient = _httpClientFactory.CreateClient();
                var url = "http://api.payamak-panel.com/post/Send.asmx/SendSimpleSMS";

                var formData = new Dictionary<string, string>
                {
                    ["username"] = username,
                    ["password"] = password,
                    ["to"] = phoneNumber,
                    ["from"] = from,
                    ["text"] = message,
                    ["isflash"] = "false"
                };

                var content = new FormUrlEncodedContent(formData);
                var response = await httpClient.PostAsync(url, content);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> SendViaTwilioAsync(string phoneNumber, string message)
        {
            try
            {
                var accountSid = _configuration["Sms:Twilio:AccountSid"];
                var authToken = _configuration["Sms:Twilio:AuthToken"];
                var fromNumber = _configuration["Sms:Twilio:FromNumber"];

                var httpClient = _httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes($"{accountSid}:{authToken}")));

                var url = $"https://api.twilio.com/2010-04-01/Accounts/{accountSid}/Messages.json";

                var formData = new Dictionary<string, string>
                {
                    ["To"] = phoneNumber,
                    ["From"] = fromNumber,
                    ["Body"] = message
                };

                var content = new FormUrlEncodedContent(formData);
                var response = await httpClient.PostAsync(url, content);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        private string FormatPhoneNumber(string phoneNumber)
        {
            var cleaned = new string(phoneNumber.Where(char.IsDigit).ToArray());

            if (cleaned.StartsWith("0"))
            {
                // تبدیل به فرمت بین‌المللی
                return "98" + cleaned.Substring(1);
            }

            if (cleaned.StartsWith("98"))
            {
                return cleaned;
            }

            // اگر شماره بین‌المللی با + شروع شده باشد
            if (phoneNumber.StartsWith("+"))
            {
                return cleaned;
            }

            return "98" + cleaned;
        }
    }

    public class KavenegarResponse
    {
        [JsonProperty("return")]
        public KavenegarReturn Return { get; set; }

        [JsonProperty("entries")]
        public List<KavenegarEntry> Entries { get; set; }
    }

    public class KavenegarReturn
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class KavenegarEntry
    {
        [JsonProperty("messageid")]
        public long MessageId { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("statustext")]
        public string StatusText { get; set; }

        [JsonProperty("sender")]
        public string Sender { get; set; }

        [JsonProperty("receptor")]
        public string Receptor { get; set; }

        [JsonProperty("date")]
        public long Date { get; set; }

        [JsonProperty("cost")]
        public int Cost { get; set; }
    }
}