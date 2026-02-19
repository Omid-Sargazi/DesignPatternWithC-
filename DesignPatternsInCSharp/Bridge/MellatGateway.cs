using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsInCSharp.Bridge
{
    public interface IPaymentGateway2
    {
        bool Pay(decimal amount, string cardInfo);
    }

    public class ZarinpalGateway : IPaymentGateway2
    {
        public bool Pay(decimal amount, string cardInfo)
        {
            Console.WriteLine($"Zarinpal: پرداخت {amount} تومان");
            return true;
        }
    }

    public class MellatGateway : IPaymentGateway2
    {
        public bool Pay(decimal amount, string cardInfo)
        {
            Console.WriteLine($"Mellat: پرداخت {amount} تومان");
            return true;
        }
    }

    public abstract class Order
    {
        protected IPaymentGateway2 _gateway;
        protected decimal _amount;

        public Order(IPaymentGateway2 gateway, decimal amount)
        {
            _gateway = gateway;
            _amount = amount;
        }

        public abstract void Checkout(string cardInfo);
    }

    public class OnlineOrder : Order
    {
        public OnlineOrder(IPaymentGateway2 gateway, decimal amount) : base(gateway, amount) { }
        public override void Checkout(string cardInfo)
        {
            Console.WriteLine("سفارش آنلاین:");
            _gateway.Pay(_amount, cardInfo);
        }
    }

    public class InStoreOrder : Order
    {
        public InStoreOrder(IPaymentGateway2 gateway, decimal amount) : base(gateway, amount) { }
        public override void Checkout(string cardInfo)
        {
            Console.WriteLine("سفارش حضوری:");
            _gateway.Pay(_amount * 0.95m, cardInfo); // 5% تخفیف حضوری
        }
    }

}
