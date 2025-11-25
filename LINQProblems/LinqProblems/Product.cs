using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQProblems.LinqProblems
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        public static void Execute()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Gaming Laptop", Price = 2000, Category = "Electronics" },
                new Product { Id = 2, Name = "Wireless Mouse", Price = 50, Category = "Electronics" },
                new Product { Id = 3, Name = "Gaming Laptop", Price = 2000, Category = "Electronics" }, // تکراری
                new Product { Id = 4, Name = "Smartphone", Price = 1200, Category = "Electronics" },
                new Product { Id = 5, Name = "Wireless Mouse", Price = 50, Category = "Electronics" }, // تکراری
                new Product { Id = 6, Name = "Gaming Laptop", Price = 2000, Category = "Electronics" }, // تکراری
                new Product { Id = 7, Name = "4K Monitor", Price = 800, Category = "Electronics" },
                new Product { Id = 8, Name = "Smartphone", Price = 1200, Category = "Electronics" }, // تکراری
                new Product { Id = 9, Name = "Keyboard", Price = 100, Category = "Electronics" },
                new Product { Id = 10, Name = "Wireless Mouse", Price = 50, Category = "Electronics" }
            };

            var duplicateProducts = products.GroupBy(p => new { p.Name, p.Price })
                .Where(g => g.Count() > 1)
                .Select(g => new
                {
                    ProductName = g.Key.Name,
                    Price = g.Key.Price,
                    DuplicateCount = g.Count(),
                    ProductIds = g.Select(p => p.Id).ToList()
                }).OrderByDescending(x => x.DuplicateCount);

            var uniqueProducts = products.GroupBy(p => new { p.Name, p.Price })
                .Select(g => g.First())
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Price)
                .ToList();
        }
    }
}
