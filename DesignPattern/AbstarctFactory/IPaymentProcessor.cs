using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.AbstarctFactory
{
    public interface IPaymentProcessor
    {
        Task ProcessAsync(decimal amount, string currency);
    }

    public interface IReceiptGenerator
    {
        string Generate(decimal amount, string currency);
    }

    public class PayPalProcessor : IPaymentProcessor
    {
        public Task ProcessAsync(decimal amount, string currency)
        {
            Console.WriteLine($"[PayPal] Paid {amount} {currency}");
            return Task.CompletedTask;
        }
    }

    public class PayPalReceipt : IReceiptGenerator
    {
        public string Generate(decimal amount, string currency)
        {
            return $"[PayPal] Receipt for {amount} {currency}";
        }
    }

    public class StripeProcessor : IPaymentProcessor
    {
        public Task ProcessAsync(decimal amount, string currency)
        {
            Console.WriteLine($"[Stripe] Paid {amount} {currency}");
            return Task.CompletedTask;
        }
    }

    public class StripeReceipt : IReceiptGenerator
    {
        public string Generate(decimal amount, string currency)
        {
            return $"[Stripe] Receipt for {amount} {currency}";
        }
    }

    public interface IPaymentSuiteFactory
    {
        IPaymentProcessor CreateProcessor();
        IReceiptGenerator CreateReceipt();
    }

    public sealed class PayPalSuiteFactory : IPaymentSuiteFactory
    {
        public IPaymentProcessor CreateProcessor()
        {
            return new PayPalProcessor();
        }

        public IReceiptGenerator CreateReceipt()
        {
            return new PayPalReceipt();
        }
    }

    public class StripeSuiteFactory : IPaymentSuiteFactory
    {
        public IPaymentProcessor CreateProcessor()
        {
            return new StripeProcessor();
        }

        public IReceiptGenerator CreateReceipt()
        {
            return new StripeReceipt();
        }
    }

    public sealed class CheckoutService
    {
        private readonly IPaymentProcessor _processor;
        private readonly IReceiptGenerator _receipt;
        public CheckoutService(IPaymentSuiteFactory factory)
        {
            _processor = factory.CreateProcessor();
            _receipt = factory.CreateReceipt();
        }

        public async Task<string> CheckoutAsync(decimal amount,string currency)
        {
            await _processor.ProcessAsync(amount, currency);
            return _receipt.Generate(amount, currency);
        }
    }
}
