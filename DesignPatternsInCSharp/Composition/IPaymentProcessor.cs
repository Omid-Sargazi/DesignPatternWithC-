using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsInCSharp.Composition
{
    public interface IPaymentProcessor { void Process(decimal amount); }
    public class BasicPaymentProcessor : IPaymentProcessor { public void Process(decimal amount) { Console.WriteLine($"Processing payment: {amount}"); } }
    public class LoggingDecorator : IPaymentProcessor { private readonly IPaymentProcessor _processor; public LoggingDecorator(IPaymentProcessor processor) { _processor = processor; } public void Process(decimal amount) { Console.WriteLine("Logging payment..."); _processor.Process(amount); } }
}


