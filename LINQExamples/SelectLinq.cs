using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQExamples
{
    public class SelectLinq
    {
        public static void Run()
        {
            int[] numbers = new[] { 1, 2, 3, 4, 5 };

            var evens = numbers.Where(n => n % 2 == 0);

            var names = new[] { "Omid", "Saeed", "Vahid", "Tom", "Slice", "Bob" };
            var longName = names.Where(n => n.Length > 4);


            var products = new List<Product> { new Product { Price = 50 }, new Product { Price = 150 } };
            var expensive = products.Where(p => p.Price > 100);

            var values = new[] { "a", null, "b" };
            var nonNulls = values.Where(v => v != null);
        }

        public class Product
        {
            public int Price;
        }
    }
}
