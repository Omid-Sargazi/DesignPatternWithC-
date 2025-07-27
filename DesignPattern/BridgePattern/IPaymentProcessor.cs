using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.BridgePattern
{
    public interface IPaymentProcessor
    {
        void ProcessPayment(decimal amount);
    }

    public class CreditCardPayment : IPaymentProcessor
    {
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"پرداخت با کارت اعتباری به مبلغ {amount} تومان انجام شد.");
        }
    }

    public class PayPalPayment : IPaymentProcessor
    {
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"پرداخت با PayPal به مبلغ {amount} تومان انجام شد.");
        }
    }

    public class BankTransferPayment : IPaymentProcessor
    {
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"پرداخت بانکی به مبلغ {amount} تومان انجام شد.");
        }
    }
}
