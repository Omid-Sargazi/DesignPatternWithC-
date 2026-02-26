using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatternsInCSharp.Bridge;

namespace DesignPatternsInCSharp.CompositionStrategyDecorator
{
    public abstract class OrderHandler
    {
        protected OrderHandler Next;

        public OrderHandler SetNext(OrderHandler next)
        {
            Next = next;
            return next;
        }

        public virtual void Handle(Order order)
        {
            Next?.Handle(order);
        }
    }

    public class ValidateHandler : OrderHandler
    {
        public override void Handle(Order order)
        {
            Console.WriteLine("Validating...");
            base.Handle(order);
        }
    }

}
