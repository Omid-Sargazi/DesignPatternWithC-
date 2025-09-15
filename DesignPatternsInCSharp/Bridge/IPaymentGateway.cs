using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsInCSharp.Bridge
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
            return "Paypal";
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
        protected readonly IPaymentGateway _gateway;

        public Payment(IPaymentGateway gateway)
        {
            _gateway = gateway;
        }

        public abstract bool ExecutePayment(decimal amount, string currency);
        public abstract bool CancelPayment(string transactionId);
        public abstract string GetPaymentType();
    }

    public class CreditCardPayment : Payment
    {
        private string _cardNumber;
        private string _expiryDate;
        public CreditCardPayment(IPaymentGateway gateway,string cardNumber, string expiryDate) : base(gateway)
        {
            _cardNumber = cardNumber;
            _expiryDate = expiryDate;
        }

        public override bool ExecutePayment(decimal amount, string currency)
        {
            Console.WriteLine($"Processing credit card payment: {_cardNumber}");
            return _gateway.ProcessPayment(amount,currency);
        }

        public override bool CancelPayment(string transactionId)
        {
            Console.WriteLine($"Canceling credit card payment: {transactionId}");
            return _gateway.RefundPayment(0, transactionId);
        }

        public override string GetPaymentType()
        {
           return "Credit Card";
        }
    }
}
