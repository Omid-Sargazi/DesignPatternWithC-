using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsWithC_.StructuralPatterns.PaySystem
{
    public interface IPaymentProcessor
    {
        bool ProcessPayment(double amount);
        bool Refund(double amount);
    }

    public class LegacyPaymentAdapter : IPaymentProcessor
    {
        private readonly LegacyPaymentSystem _legacyPaymentSystem;
        public LegacyPaymentAdapter(LegacyPaymentSystem legacyPaymentSystem)
        {
            _legacyPaymentSystem = legacyPaymentSystem;
        }
        public bool ProcessPayment(double amount)
        {
            _legacyPaymentSystem.MakeTransaction(amount);
            return true;
        }

        public bool Refund(double amount)
        {
            _legacyPaymentSystem.CancelTransaction(amount);
            return true;
        }
    }
}
