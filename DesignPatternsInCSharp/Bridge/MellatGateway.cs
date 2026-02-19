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
}
