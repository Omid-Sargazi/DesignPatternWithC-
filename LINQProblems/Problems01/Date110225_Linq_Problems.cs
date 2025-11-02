using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQProblems.Problems01
{
    public class Problems2
    {
        public static void Execute()
        {
            var products = new List<Product>
            {
                new Product { Name = "لپ‌تاپ", Category = "الکترونیک", Price = 450, Description = "لپ‌تاپ گیمینگ" },
                new Product { Name = "ماوس", Category = "الکترونیک", Price = 50, Description = null },
                new Product { Name = "کتاب", Category = "آموزشی", Price = 30, Description = "کتاب برنامه‌نویسی" },
                new Product { Name = "هدفون", Category = "الکترونیک", Price = 150, Description = "" }
            };


            var highValueCategory = products.GroupBy(p => p.Category)
                .Where(g => g.Average(p => p.Price) > 200).Select(g => new
                {
                    Category = g.Key,
                    AveragePrice = g.Average(p => p.Price),
                    Products = g.ToList()
                }).ToList();
        }
    }


    
}
