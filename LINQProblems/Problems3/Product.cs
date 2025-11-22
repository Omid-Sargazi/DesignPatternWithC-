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


            var orders = new List<Order>
            {
                new Order { OrderId = 1, CustomerName = "Ali", Amount = 100, OrderDate = new DateTime(2024, 1, 15) },
                new Order { OrderId = 2, CustomerName = "Ali", Amount = 200, OrderDate = new DateTime(2024, 1, 20) },
                new Order { OrderId = 3, CustomerName = "Maryam", Amount = 150, OrderDate = new DateTime(2024, 1, 10) },
                new Order { OrderId = 4, CustomerName = "Reza", Amount = 300, OrderDate = new DateTime(2024, 1, 25) },
                new Order { OrderId = 5, CustomerName = "Maryam", Amount = 250, OrderDate = new DateTime(2024, 2, 1) },
                new Order { OrderId = 6, CustomerName = "Ali", Amount = 50, OrderDate = new DateTime(2024, 2, 5) }
            };

            var customerStats = orders.GroupBy(o => o.CustomerName)
                .Select(g => new
                {
                    CustomerName = g.Key,
                    TotalAmount = g.Sum(o => o.Amount),
                    AverageAmount = g.Average(o => o.Amount),
                    OrderCount = g.Count(),
                    MaxOrderAmount = g.Max(o => o.Amount),
                    MinOrderAmount = g.Min(o => o.Amount)
                }).OrderByDescending(x => x.TotalAmount);
        }
    }


    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public decimal Amount { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
