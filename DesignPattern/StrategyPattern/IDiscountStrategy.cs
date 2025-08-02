using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.StrategyPattern
{
    public interface IDiscountStrategy
    {
        decimal ApplyDiscount(decimal discount);
    }

    public class RegularDiscountStrategy : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal amount)
        {
            var discount = amount * 0.5m;
            return amount - discount;
        }
    }

    public class PremiumDiscountStrategy : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal amount)
        {
            var discount = amount * 0.6m;
            return amount - discount;
        }
    }

    public class VipDiscountStrategy : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal amount)
        {
            var discount=  amount * .04m;
            return amount - discount;
            
        }
    }

    public class DiscountService
    {
        private readonly IDiscountStrategy _strategy;
        public DiscountService(IDiscountStrategy strategy)
        {
            _strategy = strategy;
        }

        public decimal ApplyDiscount(decimal amount)
        {
            return _strategy.ApplyDiscount(amount);
        }
    }
}
