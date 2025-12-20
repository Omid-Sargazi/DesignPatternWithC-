using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;

namespace API.Services
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(string email, string confirmationLink, string fullName);
        Task SendPasswordResetEmailAsync(string email, string resetLink, string code, string fullName);
        Task SendWelcomeEmailAsync(string email, string fullName, string username);
        Task SendTwoFactorCodeAsync(string email, string code);
        Task SendPasswordChangedNotificationAsync(string email, string fullName);
        Task SendSecurityAlertAsync(string email, string alertType, string ipAddress, string deviceInfo);
    }

    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FromName { get; set; }
        public string FromAddress { get; set; }
        public bool EnableSsl { get; set; }
    }

    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        private readonly ILogger<EmailService> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public EmailService(
            IOptions<EmailSettings> settings,
            ILogger<EmailService> logger,
            IWebHostEnvironment env,
            IConfiguration configuration)
        {
            _settings = settings.Value;
            _logger = logger;
            _env = env;
            _configuration = configuration;
        }

        public async Task SendConfirmationEmailAsync(string email, string confirmationLink, string fullName)
        {
            var subject = "تایید ایمیل حساب کاربری";
            var templateData = new
            {
                FullName = fullName,
                ConfirmationLink = confirmationLink,
                Year = DateTime.Now.Year,
                AppName = _configuration["AppSettings:Name"] ?? "اپلیکیشن ما",
                SupportEmail = _configuration["AppSettings:SupportEmail"] ?? "support@example.com",
                ExpirationHours = 24
            };

            var htmlContent = await RenderTemplateAsync("EmailTemplates/ConfirmationEmail.cshtml", templateData);
            var textContent = $"سلام {fullName}\n\nلطفاً برای تایید ایمیل خود روی لینک زیر کلیک کنید:\n{confirmationLink}\n\nاین لینک ۲۴ ساعت اعتبار دارد.";

            await SendEmailAsync(email, subject, htmlContent, textContent);

            _logger.LogInformation("ایمیل تایید برای {Email} ارسال شد", email);
        }

        public async Task SendPasswordResetEmailAsync(string email, string resetLink, string code, string fullName)
        {
            var subject = "بازیابی رمز عبور";
            var templateData = new
            {
                FullName = fullName,
                ResetLink = resetLink,
                Code = code,
                Year = DateTime.Now.Year,
                AppName = _configuration["AppSettings:Name"] ?? "اپلیکیشن ما",
                IpAddress = "IP ناشناس",
                Browser = "مرورگر ناشناس",
                Time = DateTime.Now.ToString("yyyy/MM/dd HH:mm"),
                ExpirationMinutes = 10
            };

            var htmlContent = await RenderTemplateAsync("EmailTemplates/PasswordResetEmail.cshtml", templateData);
            var textContent = $"سلام {fullName}\n\nکد بازیابی رمز عبور: {code}\n\nیا از لینک زیر استفاده کنید:\n{resetLink}\n\nاین لینک ۱۰ دقیقه اعتبار دارد.";

            await SendEmailAsync(email, subject, htmlContent, textContent);

            _logger.LogInformation("ایمیل بازیابی رمز عبور برای {Email} ارسال شد", email);
        }

        public async Task SendWelcomeEmailAsync(string email, string fullName, string username)
        {
            var subject = "خوش آمدید به اپلیکیشن ما";
            var templateData = new
            {
                FullName = fullName,
                Username = username,
                Year = DateTime.Now.Year,
                AppName = _configuration["AppSettings:Name"] ?? "اپلیکیشن ما",
                LoginUrl = _configuration["Frontend:LoginUrl"] ?? "https://app.example.com/login",
                SupportEmail = _configuration["AppSettings:SupportEmail"] ?? "support@example.com",
                Features = new[]
                {
                    "پروفایل شخصی",
                    "تنظیمات امنیتی",
                    "پشتیبانی ۲۴/۷",
                    "بروزرسانی‌های منظم"
                }
            };

            var htmlContent = await RenderTemplateAsync("EmailTemplates/WelcomeEmail.cshtml", templateData);
            var textContent = $"سلام {fullName}\n\nبه {templateData.AppName} خوش آمدید!\n\nنام کاربری شما: {username}\n\nبا تشکر از انتخاب شما.";

            await SendEmailAsync(email, subject, htmlContent, textContent);

            _logger.LogInformation("ایمیل خوش‌آمدگویی برای {Email} ارسال شد", email);
        }

        public async Task SendTwoFactorCodeAsync(string email, string code)
        {
            var subject = "کد تایید دو مرحله‌ای";
            var templateData = new
            {
                Code = code,
                Year = DateTime.Now.Year,
                AppName = _configuration["AppSettings:Name"] ?? "اپلیکیشن ما",
                Time = DateTime.Now.ToString("HH:mm"),
                Date = DateTime.Now.ToString("yyyy/MM/dd"),
                IpAddress = "IP ناشناس",
                ExpirationMinutes = 5
            };

            var htmlContent = await RenderTemplateAsync("EmailTemplates/TwoFactorEmail.cshtml", templateData);
            var textContent = $"کد تایید دو مرحله‌ای شما: {code}\n\nاین کد ۵ دقیقه اعتبار دارد.";

            await SendEmailAsync(email, subject, htmlContent, textContent);

            _logger.LogInformation("کد دو مرحله‌ای برای {Email} ارسال شد", email);
        }

        public async Task SendPasswordChangedNotificationAsync(string email, string fullName)
        {
            var subject = "تغییر رمز عبور";
            var templateData = new
            {
                FullName = fullName,
                Year = DateTime.Now.Year,
                AppName = _configuration["AppSettings:Name"] ?? "اپلیکیشن ما",
                Time = DateTime.Now.ToString("yyyy/MM/dd HH:mm"),
                IpAddress = "IP ناشناس",
                Device = "دستگاه ناشناس",
                SupportEmail = _configuration["AppSettings:SupportEmail"] ?? "support@example.com"
            };

            var htmlContent = await RenderTemplateAsync("EmailTemplates/PasswordChangedEmail.cshtml", templateData);
            var textContent = $"سلام {fullName}\n\nرمز عبور حساب شما با موفقیت تغییر یافت.\n\nاگر شما این تغییر را انجام نداده‌اید، لطفاً بلافاصله با پشتیبانی تماس بگیرید.";

            await SendEmailAsync(email, subject, htmlContent, textContent);

            _logger.LogInformation("ایمیل تغییر رمز عبور برای {Email} ارسال شد", email);
        }

        public async Task SendSecurityAlertAsync(string email, string alertType, string ipAddress, string deviceInfo)
        {
            var subject = $"هشدار امنیتی: {alertType}";
            var templateData = new
            {
                AlertType = alertType,
                IpAddress = ipAddress,
                DeviceInfo = deviceInfo,
                Year = DateTime.Now.Year,
                AppName = _configuration["AppSettings:Name"] ?? "اپلیکیشن ما",
                Time = DateTime.Now.ToString("yyyy/MM/dd HH:mm"),
                SupportEmail = _configuration["AppSettings:SupportEmail"] ?? "support@example.com",
                ActionRequired = alertType.Contains("مشکوک") ? "توصیه می‌شود رمز عبور خود را تغییر دهید." : ""
            };

            var htmlContent = await RenderTemplateAsync("EmailTemplates/SecurityAlertEmail.cshtml", templateData);
            var textContent = $"هشدار امنیتی: {alertType}\n\nآی‌پی: {ipAddress}\n\nدستگاه: {deviceInfo}\n\nزمان: {DateTime.Now:yyyy/MM/dd HH:mm}";

            await SendEmailAsync(email, subject, htmlContent, textContent);

            _logger.LogWarning("ایمیل هشدار امنیتی برای {Email} ارسال شد: {AlertType}", email, alertType);
        }

        private async Task SendEmailAsync(string toEmail, string subject, string htmlContent, string textContent = null)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_settings.FromName, _settings.FromAddress));
                message.To.Add(new MailboxAddress("", toEmail));
                message.Subject = subject;

                var builder = new BodyBuilder
                {
                    HtmlBody = htmlContent
                };

                if (!string.IsNullOrEmpty(textContent))
                {
                    builder.TextBody = textContent;
                }

                message.Body = builder.ToMessageBody();

                //using var client = new SmtpClient();

                if (_env.IsDevelopment())
                {
                    // ذخیره ایمیل به صورت فایل
                    await message.WriteToAsync($"emails/{Guid.NewGuid()}.eml");
                    _logger.LogInformation("ایمیل در حالت توسعه ذخیره شد: {Subject}", subject);
                    return;
                }

                //await client.ConnectAsync(_settings.SmtpServer, _settings.Port,
                //    _settings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None);

                //await client.AuthenticateAsync(_settings.Username, _settings.Password);
                //await client.SendAsync(message);
                //await client.DisconnectAsync(true);

                _logger.LogDebug("ایمیل به {Email} ارسال شد", toEmail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در ارسال ایمیل به {Email}", toEmail);
                throw;
            }
        }

        private async Task<string> RenderTemplateAsync(string templatePath, object model)
        {
            try
            {
                var templateFile = Path.Combine(_env.ContentRootPath, "Templates", templatePath);

                if (!File.Exists(templateFile))
                {
                    // استفاده از تمپلیت پیش‌فرض
                    return await GetDefaultTemplateAsync(templatePath, model);
                }

                var template = await File.ReadAllTextAsync(templateFile);

                // جایگزینی مقادیر در تمپلیت
                foreach (var prop in model.GetType().GetProperties())
                {
                    var placeholder = $"{{{{{prop.Name}}}}}";
                    var value = prop.GetValue(model)?.ToString() ?? "";
                    template = template.Replace(placeholder, value);
                }

                return template;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در رندر کردن تمپلیت {TemplatePath}", templatePath);
                return await GetDefaultTemplateAsync(templatePath, model);
            }
        }

        private async Task<string> GetDefaultTemplateAsync(string templateName, object model)
        {
            // تمپلیت‌های پیش‌فرض HTML
            var templates = new Dictionary<string, string>
            {
                ["ConfirmationEmail"] = """
                    <!DOCTYPE html>
                    <html dir="rtl">
                    <head>
                        <meta charset="utf-8">
                        <style>
                            body { font-family: Tahoma, sans-serif; line-height: 1.6; color: #333; }
                            .container { max-width: 600px; margin: 0 auto; padding: 20px; }
                            .header { background: #4CAF50; color: white; padding: 20px; text-align: center; }
                            .content { background: #f9f9f9; padding: 30px; border-radius: 5px; }
                            .button { display: inline-block; background: #4CAF50; color: white; 
                                     padding: 12px 24px; text-decoration: none; border-radius: 4px; 
                                     margin: 20px 0; }
                            .footer { margin-top: 30px; padding-top: 20px; border-top: 1px solid #ddd; 
                                     text-align: center; color: #666; }
                        </style>
                    </head>
                    <body>
                        <div class="container">
                            <div class="header">
                                <h1>{{AppName}}</h1>
                            </div>
                            <div class="content">
                                <h2>سلام {{FullName}} عزیز</h2>
                                <p>برای فعال‌سازی حساب کاربری خود، روی دکمه زیر کلیک کنید:</p>
                                <p style="text-align: center;">
                                    <a href="{{ConfirmationLink}}" class="button">تایید ایمیل</a>
                                </p>
                                <p>یا لینک زیر را در مرورگر خود کپی کنید:</p>
                                <p style="direction: ltr; word-break: break-all; background: #eee; 
                                   padding: 10px; border-radius: 3px;">
                                    {{ConfirmationLink}}
                                </p>
                                <p>این لینک تا {{ExpirationHours}} ساعت آینده معتبر است.</p>
                            </div>
                            <div class="footer">
                                <p>© {{Year}} {{AppName}}. تمامی حقوق محفوظ است.</p>
                                <p>سوالی دارید؟ با {{SupportEmail}} تماس بگیرید.</p>
                            </div>
                        </div>
                    </body>
                    </html>
                    """,
                // سایر تمپلیت‌ها...
            };

            if (templates.TryGetValue(Path.GetFileNameWithoutExtension(templateName), out var template))
            {
                foreach (var prop in model.GetType().GetProperties())
                {
                    var placeholder = $"{{{{{prop.Name}}}}}";
                    var value = prop.GetValue(model)?.ToString() ?? "";
                    template = template.Replace(placeholder, value);
                }
                return template;
            }

            return "<h1>ایمیل</h1><p>متن ایمیل</p>";
        }
    }
}