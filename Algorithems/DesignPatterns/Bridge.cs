using System;
using System.Collections.Generic;
using System.Linq;
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

}
