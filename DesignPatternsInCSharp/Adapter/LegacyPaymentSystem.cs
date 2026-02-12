using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsInCSharp.Adapter
{
    public class LegacyPaymentSystem
    {
        public void ProcessOldPayment(string accountNumber, decimal amount)
        {
            Console.WriteLine($"پرداخت قدیمی: {amount} تومان از حساب {accountNumber}");
        }
    }

    // رابط جدید که سیستم ما انتظار دارد
    public interface IModernPaymentProcessor
    {
        void ProcessPayment(string customerId, decimal amount, string currency);
        bool ValidateTransaction(string customerId);
    }

    // Adapter برای تبدیل سیستم قدیمی به رابط جدید
    public class PaymentAdapter : IModernPaymentProcessor
    {
        private readonly LegacyPaymentSystem _legacySystem;

        public PaymentAdapter(LegacyPaymentSystem legacySystem)
        {
            _legacySystem = legacySystem;
        }

        public void ProcessPayment(string customerId, decimal amount, string currency)
        {
            // تبدیل ارز به تومان
            decimal amountInToman = currency == "USD" ? amount * 50000 : amount;

            // فراخوانی متد قدیمی
            _legacySystem.ProcessOldPayment(customerId, amountInToman);
        }

        public bool ValidateTransaction(string customerId)
        {
            // اعتبارسنجی ساده
            return !string.IsNullOrEmpty(customerId);
        }
    }

}
