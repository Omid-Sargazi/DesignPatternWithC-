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

    public abstract class PaymentManager
    {
        protected IPaymentProcessor _processor;
        protected PaymentManager(IPaymentProcessor processor)
        {
            _processor = processor;
        }

        public abstract void ProcessPayment(decimal amount);
    }

    public class WebPaymentManager : PaymentManager
    {
       
       
        public WebPaymentManager(IPaymentProcessor processor) : base(processor)
        {
          
        }

        public override void ProcessPayment(decimal amount)
        {
            Console.WriteLine("پرداخت در پلتفرم وب:");
            _processor.ProcessPayment(amount);
        }
    }

    public class MobilePaymentManager : PaymentManager
    {
     
        public MobilePaymentManager(IPaymentProcessor processor) : base(processor)
        {
           
        }

        public override void ProcessPayment(decimal amount)
        {
            Console.WriteLine("پرداخت در اپلیکیشن موبایل:");
            _processor.ProcessPayment(amount);
        }
    }


    public class ClientPayment
    {
        public static void RunPayment()
        {
            IPaymentProcessor creditCard = new CreditCardPayment();
            IPaymentProcessor paypal = new PayPalPayment();
            IPaymentProcessor bankTransfer = new BankTransferPayment();

            PaymentManager webCreditCard = new WebPaymentManager(creditCard);
            PaymentManager mobilePaypal = new MobilePaymentManager(paypal);

            webCreditCard.ProcessPayment(1000);
            mobilePaypal.ProcessPayment(1000);


        }
    }
}
