using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsInCSharp.CompositionStrategyDecorator
{
    public interface IPaymentStrategy
    {
        void Pay(decimal amount);
    }

    public class PaypalPayment : IPaymentStrategy
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine("PayPal payment");
        }
    }

    public class LoggingDecorator : IPaymentStrategy
    {
        private readonly IPaymentStrategy _inner;
        public LoggingDecorator(IPaymentStrategy inner) => _inner = inner;

        public void Pay(decimal amount) { Console.WriteLine("Logging..."); _inner.Pay(amount); }
    }

    public class PaymentProcessor
    {
        public IPaymentStrategy Strategy { get; set; }
        public void Process(decimal amount) => Strategy.Pay(amount);
    }
}
