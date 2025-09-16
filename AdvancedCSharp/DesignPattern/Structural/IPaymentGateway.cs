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
}
