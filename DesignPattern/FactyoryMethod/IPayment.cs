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

    public class FactoryMethod
    {
        public static void Run()
        {
            string method = "paypal";
            IPayment payment;
            if(method == "paypal")
            {
                payment = new PayPalPayment();
            }
           else
            {
                payment = new StripePayment();
            }

            payment.Pay(100);
        }
    }
}
