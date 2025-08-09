using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.FactyoryMethod
{
    public interface IPayment
    {
        void Pay(decimal amount);
    }

    public class PayPalPayment : IPayment
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine($"Paid {amount} via PayPal");
        }
    }

    public class StripePayment : IPayment
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine($"Paid {amount} via Stripe");
        }
    }

    public abstract class PaymentFactory
    {
        public abstract IPayment CreatePayment();
    }

    public class PayPalFactory : PaymentFactory
    {
        public PayPalFactory()
        {
        }

        public override IPayment CreatePayment()
        {
           return new PayPalPayment();
        }
    }

    public class StripeFactory : PaymentFactory
    {
        public override IPayment CreatePayment()
        {
            return new StripePayment();
        }
    }

    public class FactoryMethod
    {
        public static void Run()
        {
            PaymentFactory factory = new PayPalFactory();
            var paymnet = factory.CreatePayment();

            paymnet.Pay(100);
        }
    }
}
