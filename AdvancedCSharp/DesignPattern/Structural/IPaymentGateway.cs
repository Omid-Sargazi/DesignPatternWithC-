using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCSharp.DesignPattern.Structural
{
    public interface IPaymentGateway
    {
        bool ProcessPayment(decimal amount, string currency);
        bool RefundPayment(decimal amount, string transactionId);
        string GetGatewayName();
    }
}
