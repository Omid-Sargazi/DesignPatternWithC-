using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsInCSharp.Adapter
{
    public interface ISmsService
    {
        bool SendSms(string phoneNumber, string message);
        bool SendBulkSms(List<string> phoneNumbers, string message);
    }

    // سرویس کاوه‌نگار
    public class KavenegarApi
    {
        public int Send(string receptor, string message)
        {
            Console.WriteLine($"کاوه‌نگار: ارسال به {receptor}");
            Console.WriteLine($"متن: {message}");
            return 200; // کد موفقیت
        }

        public int[] SendArray(string[] receptors, string message)
        {
            Console.WriteLine($"کاوه‌نگار: ارسال گروهی به {receptors.Length} شماره");
            return new int[receptors.Length];
        }
    }

    // سرویس ملی‌پیامک
    public class MeliPayamakApi
    {
        public string SendSimpleSms(string to, string text, string from)
        {
            Console.WriteLine($"ملی‌پیامک: از {from} به {to}");
            Console.WriteLine($"پیام: {text}");
            return "SUCCESS";
        }
    }

    // Adapter برای کاوه‌نگار
    public class KavenegarAdapter : ISmsService
    {
        private readonly KavenegarApi _api;
        private const string DefaultSender = "10004346";

        public KavenegarAdapter(KavenegarApi api)
        {
            _api = api;
        }

        public bool SendSms(string phoneNumber, string message)
        {
            int result = _api.Send(phoneNumber, message);
            return result == 200;
        }

        public bool SendBulkSms(List<string> phoneNumbers, string message)
        {
            int[] results = _api.SendArray(phoneNumbers.ToArray(), message);
            return results.All(r => r == 200);
        }
    }

    // Adapter برای ملی‌پیامک
    public class MeliPayamakAdapter : ISmsService
    {
        private readonly MeliPayamakApi _api;
        private const string SenderNumber = "3000505";

        public MeliPayamakAdapter(MeliPayamakApi api)
        {
            _api = api;
        }

        public bool SendSms(string phoneNumber, string message)
        {
            string result = _api.SendSimpleSms(phoneNumber, message, SenderNumber);
            return result == "SUCCESS";
        }

        public bool SendBulkSms(List<string> phoneNumbers, string message)
        {
            foreach (var phone in phoneNumbers)
            {
                string result = _api.SendSimpleSms(phone, message, SenderNumber);
                if (result != "SUCCESS") return false;
            }
            return true;
        }
    }

    // استفاده در سیستم احراز هویت
    public class AuthenticationService
    {
        private readonly ISmsService _smsService;

        public AuthenticationService(ISmsService smsService)
        {
            _smsService = smsService;
        }

        public void SendVerificationCode(string phoneNumber)
        {
            string code = new Random().Next(1000, 9999).ToString();
            string message = $"کد تایید شما: {code}";

            bool sent = _smsService.SendSms(phoneNumber, message);
            Console.WriteLine(sent ? "✓ کد ارسال شد" : "✗ خطا در ارسال");
        }
    }
}
