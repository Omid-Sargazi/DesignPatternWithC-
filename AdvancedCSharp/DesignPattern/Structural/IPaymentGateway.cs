using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCSharp.DesignPattern.Structural
{
    public interface IPaymentGateway
    {
        bool ProcessPayment(decimal amount, string currency);
        bool RefundPayment(decimal amount, string transactionId);
        string GetGatewayName();
    }

    public class PayPalGateway : IPaymentGateway
    {
        public bool ProcessPayment(decimal amount, string currency)
        {
            Console.WriteLine($"Processing ${amount} {currency} via PayPal");
            return true;
        }

        public bool RefundPayment(decimal amount, string transactionId)
        {
            Console.WriteLine($"Refunding ${amount} via PayPal for transaction {transactionId}");
            return true;
        }

        public string GetGatewayName()
        {
            return "PayPal";
        }
    }

    public class StripeGateway : IPaymentGateway
    {
        public bool ProcessPayment(decimal amount, string currency)
        {
            Console.WriteLine($"Processing ${amount} {currency} via Stripe");
            return true;
        }

        public bool RefundPayment(decimal amount, string transactionId)
        {
            Console.WriteLine($"Refunding ${amount} via Stripe for transaction {transactionId}");
            return true;
        }

        public string GetGatewayName()
        {
            return "Stripe";
        }
    }

    public abstract class Payment
    {
        protected readonly IPaymentGateway _paymentGateway;

        public Payment(IPaymentGateway paymentGateway)
        {
            _paymentGateway = paymentGateway;
        }

        public abstract bool ExecutePayment(decimal amount, string currency);
        public abstract bool CancelPayment(string transactionId);
        public abstract string GetPaymentType();
    }

    public class CreditCardPayment : Payment
    {
        private string _cartNumber;
        private string _expiryDate;
        public CreditCardPayment(IPaymentGateway paymentGateway, string cartNumber, string expiryDate) : base(paymentGateway)
        {
            _cartNumber = cartNumber;
            _expiryDate = expiryDate;
        }

        public override bool ExecutePayment(decimal amount, string currency)
        {
            Console.WriteLine($"Processing credit card payment: {_cartNumber}");
            return _paymentGateway.ProcessPayment(amount, currency);

        }

        public override bool CancelPayment(string transactionId)
        {
            Console.WriteLine($"Canceling credit card payment: {transactionId}");
            return _paymentGateway.RefundPayment(0, transactionId);
        }

        public override string GetPaymentType()
        {
            return "Credit Card";
        }
    }

    public class CryptoPayment : Payment
    {
        private string _walletAddress;
        public CryptoPayment(IPaymentGateway paymentGateway, string walletAddress) : base(paymentGateway)
        {
            _walletAddress = walletAddress;
        }

        public override bool ExecutePayment(decimal amount, string currency)
        {
            Console.WriteLine($"Processing crypto payment from: {_walletAddress}");
            return _paymentGateway.ProcessPayment(amount, currency);
        }

        public override bool CancelPayment(string transactionId)
        {
            Console.WriteLine($"Canceling crypto payment: {transactionId}");
            return _paymentGateway.RefundPayment(0, transactionId);
        }

        public override string GetPaymentType()
        {
            return "Cryptocurrency";
        }
    }

    public class Clientpayment
    {
        public static void Run()
        {
            IPaymentGateway paypal = new PayPalGateway();
            IPaymentGateway strip = new StripeGateway();

            Payment crediteCardPayent = new CreditCardPayment(paypal, "1234-5678-9012-3456", "12/2");
            crediteCardPayent.ExecutePayment(100, "USD");
        }
    }
}
