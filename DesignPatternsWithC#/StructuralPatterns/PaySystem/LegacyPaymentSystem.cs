using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsWithC_.StructuralPatterns.PaySystem
{
    public class LegacyPaymentSystem
    {
        public bool MakeTransaction(double amount)
        {
            Console.WriteLine($"Legacy system processing transaction of ${amount}");
            return true;
        }
        public bool CancelTransaction(double amount) 
        {
            Console.WriteLine($"Legacy system canceling transaction of ${amount}");
            return true;
        }
    }
}
