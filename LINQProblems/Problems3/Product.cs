using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQProblems.Problems3
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
                new Product { Id = 1, Name = "Laptop", Price = 1500, Category = "Electronics" },
                new Product { Id = 2, Name = "Mouse", Price = 50, Category = "Electronics" },
                new Product { Id = 3, Name = "Desk", Price = 800, Category = "Furniture" },
                new Product { Id = 4, Name = "Monitor", Price = 1200, Category = "Electronics" },
                new Product { Id = 5, Name = "Chair", Price = 300, Category = "Furniture" }
            };

            var expensiveProducts = products.Where(p => p.Price > 1000)
                .OrderByDescending(p => p).Select(p => new { p.Name, p.Price });
        }
    }
}
