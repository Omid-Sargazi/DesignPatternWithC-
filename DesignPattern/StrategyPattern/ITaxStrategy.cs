using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.StrategyPattern
{
    public interface ITaxStrategy
    {
        decimal Calculate(decimal amount);
    }

    public class IranTaxStrategy : ITaxStrategy
    {
        public decimal Calculate(decimal amount)
        {
            return amount + amount * 0.06m;
        }
    }

    public class USATaxStrategy : ITaxStrategy
    {
        public decimal Calculate(decimal amount)
        {
            return amount + amount * 0.09m;
        }
    }

    public class GermanyTaxStrategy : ITaxStrategy
    {
        public decimal Calculate(decimal amount)
        {
            return amount + amount * 0.6m;
        }
    }

    public class TaxCalculator
    {
        private ITaxStrategy _strategy;
        public TaxCalculator(ITaxStrategy strategy)
        {
            _strategy = strategy;
        }

        public decimal Calculate(decimal amount)
        {
            return _strategy.Calculate(amount);
        }
    }

    public class ClientTax
    {
        public static void Run()
        {
            var IRTax = new IranTaxStrategy();
            var USATax = new USATaxStrategy();

            var calculate = new TaxCalculator(IRTax);
            Console.WriteLine(calculate.Calculate(100));

            calculate = new TaxCalculator(USATax);

           Console.WriteLine(calculate.Calculate(100));
        }
    }
}
