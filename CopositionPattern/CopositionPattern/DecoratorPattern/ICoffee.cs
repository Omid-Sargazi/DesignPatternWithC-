using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopositionPattern.DecoratorPattern
{
    public interface ICoffee
    {
        string GetDescription();
        double GetCost();

    }

    public class BasicCoffee : ICoffee
    {
        public double GetCost()
        {
            return 1.5;
        }

        public string GetDescription()
        {
            return "Simple Coffee ";
        }
    }

    public class WithMilk : ICoffee
    {
        private readonly ICoffee _coffee;
        public WithMilk(ICoffee coffee)
        {
            _coffee = coffee;
        }
        public double GetCost()
        {
           return _coffee.GetCost() + 1.1;
        }

        public string GetDescription()
        {
            return _coffee.GetDescription() + " + Milk";
        }
    }

    public class WithSugre : ICoffee
    {
        private readonly ICoffee _coffee;
        public WithSugre(ICoffee coffee)
        {
            _coffee=coffee;
        }

        public double GetCost()
        {
            return _coffee.GetCost() + 0.8;
        }

        public string GetDescription()
        {
            return _coffee.GetDescription() + " + Sugare";
        }
    }

    public class ClientCoffee
    {
        public static void Run()
        {
            ICoffee coffee = new BasicCoffee();
            coffee = new WithMilk(coffee);
            coffee = new WithSugre(coffee);

            Console.WriteLine(coffee.GetCost());
            Console.WriteLine(coffee.GetDescription());
        }
    }
}
