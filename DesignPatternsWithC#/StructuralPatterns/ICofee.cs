using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsWithC_.StructuralPatterns
{
    public interface ICoffee
    {
        decimal SetPrice();
        string Display();
    }

    public class SimpleCoffee : ICoffee
    {
        public decimal SetPrice()
        {
            return 1m;
        }

        public string Display()
        {
            return $"Simple Coffee";
        }
    }

    public abstract class CoffeeDecorator : ICoffee
    {
        protected ICoffee _coffee;

        public CoffeeDecorator(ICoffee coffee)
        {
            _coffee = coffee;
        }
        public virtual decimal SetPrice() => _coffee.SetPrice();

        public virtual string Display() => _coffee.Display();
    }

    public class MilkCoffee : CoffeeDecorator
    {
        public MilkCoffee(ICoffee coffee) : base(coffee)
        {
        }

        public override string Display()
        {
            return base.Display() + "Milk";
        }

        public override decimal SetPrice()
        {
            return base.SetPrice()+1m;
        }
    }

    public class Cappucino : CoffeeDecorator
    {
        public Cappucino(ICoffee coffee) : base(coffee)
        {
        }

        public override string Display()
        {
            return base.Display() + "Add Some Copucino";
        }

        public override decimal SetPrice()
        {
            return base.SetPrice() + 2m;
        }
    }
}
