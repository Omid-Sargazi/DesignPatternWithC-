using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAndDesignPattern.BehavioralDesignPattern.ChainOfResponsibility
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        object Handle(object request);
    }

    public abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
           _nextHandler = handler;
           return handler;
        }

        public virtual object Handle(object request)
        {
            if (_nextHandler != null)
            {
                return _nextHandler.Handle(request);
            }
            else
            {

                return null;
            }
        }
    }

    public class MonkeyHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if ((request as string) == "Banana")
            {
                return $"Monkey: I'll eat the {request.ToString()}.\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }

    class SquirrelHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request.ToString() == "Nut")
            {
                return $"Squirrel: I'll eat the {request.ToString()}.\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }

    class DogHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request.ToString() == "MeatBall")
            {
                return $"Dog: I'll eat the {request.ToString()}.\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }

    public class Customer
    {
        public bool IsVIP { get; set; }
        public bool IsRegular { get; set; }
        public bool ISNew { get; set; }
    }

    public class ClientDiscount
    {
        public decimal CalculateDiscount(Customer customer, decimal orderTotal)
        {
            return customer switch
            {
                {IsVIP:true}=>orderTotal*0.8m,
                {IsRegular:true}=>orderTotal*0.9m,
                {ISNew:true}=>orderTotal*.095m,
            };
        }
    }


    public abstract class DiscountHandler
    {
        protected DiscountHandler _nextHandler;

        public DiscountHandler SetNextHandler(DiscountHandler nextHandler)
        {
            _nextHandler = nextHandler;
            return _nextHandler;
        }

        public abstract decimal ClculateDiscount(Customer customer, decimal orderTotal);
    }

    public class VIPDiscountHandler:DiscountHandler
    {
        public override decimal ClculateDiscount(Customer customer, decimal orderTotal)
        {
            if (customer.IsVIP)
            {
                return orderTotal * 0.8m;
            }

            return _nextHandler.ClculateDiscount(customer, orderTotal);
        }
    }
}
