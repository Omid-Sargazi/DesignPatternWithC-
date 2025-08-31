using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Algorithems.DesignPatterns
{
    public interface IPaymentProcessor
    {
        void ProcessPaymnet(decimal amount);
    }

    public class CreditCardPayment : IPaymentProcessor
    {
        public void ProcessPaymnet(decimal amount)
        {
            Console.WriteLine($"Pay {amount} with credit card.");
        }
    }

    public class PaypalPayment : IPaymentProcessor
    {
        public void ProcessPaymnet(decimal amount)
        {
            Console.WriteLine($"Pay {amount} with paypal card.");

        }
    }

    public class BankTransferPayment : IPaymentProcessor
    {
        public void ProcessPaymnet(decimal amount)
        {
            Console.WriteLine($"Pay {amount} with BankTransfer card.");

        }
    }

    public abstract class Payment
    {
        protected readonly IPaymentProcessor _paymentProcessor;

        public Payment(IPaymentProcessor paymentProcessor)
        {
            _paymentProcessor = paymentProcessor;
        }

        public abstract void Pay(decimal amount);
    }


    public class WebPaymentManager : Payment
    {
        public WebPaymentManager(IPaymentProcessor paymentProcessor) : base(paymentProcessor)
        {
        }

        public override void Pay(decimal amount)
        {
            _paymentProcessor.ProcessPaymnet(amount);
        }
    }

    public class MobilePaymentManager : Payment
    {
        public MobilePaymentManager(IPaymentProcessor paymentProcessor) : base(paymentProcessor)
        {
        }

        public override void Pay(decimal amount)
        {
            _paymentProcessor.ProcessPaymnet(amount);
        }
    }

    public class ClientPayment
    {
        public static void Run()
        {
            IPaymentProcessor creditCard = new CreditCardPayment();
            IPaymentProcessor paypal = new PaypalPayment();
            IPaymentProcessor banktransfer = new BankTransferPayment();

            Payment webCreditcard = new WebPaymentManager(creditCard);
            Payment mobilePaypal = new MobilePaymentManager(paypal);

            webCreditcard.Pay(12m);

            mobilePaypal.Pay(41m);
        }
    }
}
