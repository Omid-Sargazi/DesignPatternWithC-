using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQExamples.LinqProblems1
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
    }

    public class ExecuteLinq2
    {
        public static void Run()
        {

            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Price = 1500, Quantity = 5, Category = "Electronics" },
                new Product { Id = 2, Name = "Mouse", Price = 25, Quantity = 50, Category = "Electronics" },
                new Product { Id = 3, Name = "Notebook", Price = 5, Quantity = 200, Category = "Stationery" },
                new Product { Id = 4, Name = "Pen", Price = 2, Quantity = 15, Category = "Stationery" },
                new Product { Id = 5, Name = "Headphones", Price = 80, Quantity = 8, Category = "Electronics" },
                new Product { Id = 6, Name = "Chair", Price = 120, Quantity = 3, Category = "Furniture" },
                new Product { Id = 7, Name = "Desk", Price = 250, Quantity = 12, Category = "Furniture" },
                new Product { Id = 8, Name = "Monitor", Price = 300, Quantity = 7, Category = "Electronics" }
            };

            var lowStockProducts = products
           .Where(p => p.Quantity < 10)
           .OrderBy(p => p.Quantity)
           .Select(p => new
           {
               p.Name,
               p.Category,
               p.Quantity,
               Status = p.Quantity < 5 ? "CRITICAL" : "LOW"
           })
           .ToList();

            Console.WriteLine("=== Low Stock Products (< 10 items) ===");
            foreach (var product in lowStockProducts)
            {
                Console.WriteLine($"{product.Name} ({product.Category}): {product.Quantity} items - {product.Status}");
            }

            // 2. مرتب‌سازی بر اساس قیمت
            var sortedByPrice = products
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    p.Name,
                    Price = $"${p.Price}",
                    p.Quantity,
                    p.Category
                })
                .ToList();

            Console.WriteLine("\n=== Products Sorted by Price ===");
            foreach (var product in sortedByPrice)
            {
                Console.WriteLine($"{product.Name}: {product.Price} - {product.Quantity} in stock");
            }

            // 3. محاسبه ارزش موجودی
            var inventoryValue = products
                .Select(p => new
                {
                    p.Name,
                    TotalValue = p.Price * p.Quantity
                })
                .OrderByDescending(p => p.TotalValue)
                .ToList();

            Console.WriteLine("\n=== Inventory Value by Product ===");
            foreach (var product in inventoryValue)
            {
                Console.WriteLine($"{product.Name}: ${product.TotalValue}");
            }

            // 4. محصولات گروه‌بندی شده بر اساس دسته
            var productsByCategory = products
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Products = g.OrderBy(p => p.Name).ToList(),
                    TotalItems = g.Sum(p => p.Quantity),
                    TotalValue = g.Sum(p => p.Price * p.Quantity)
                })
                .OrderBy(g => g.Category)
                .ToList();

            Console.WriteLine("\n=== Products by Category ===");
            foreach (var category in productsByCategory)
            {
                Console.WriteLine($"\n{category.Category}:");
                Console.WriteLine($"Total Items: {category.TotalItems}");
                Console.WriteLine($"Total Value: ${category.TotalValue}");

                foreach (var product in category.Products)
                {
                    Console.WriteLine($"  - {product.Name}: ${product.Price} × {product.Quantity} = ${product.Price * product.Quantity}");
                }
            }

            // 5. آمار کلی
            var totalItems = products.Sum(p => p.Quantity);
            var totalValue = products.Sum(p => p.Price * p.Quantity);
            var averagePrice = products.Average(p => p.Price);
            var mostExpensive = products.OrderByDescending(p => p.Price).First();

            Console.WriteLine("\n=== Inventory Summary ===");
            Console.WriteLine($"Total Products: {products.Count}");
            Console.WriteLine($"Total Items in Stock: {totalItems}");
            Console.WriteLine($"Total Inventory Value: ${totalValue}");
            Console.WriteLine($"Average Product Price: ${averagePrice:F2}");
            Console.WriteLine($"Most Expensive Product: {mostExpensive.Name} (${mostExpensive.Price})");



            int[] dailySales = { 120, 85, 110, 95, 130, 145, 80, 115, 125, 100, 90, 140 };

            Console.WriteLine("Daily Sales: " + string.Join(", ", dailySales));

            // 1. بیشترین و کمترین فروش
            int maxSale = dailySales.Max();
            int minSale = dailySales.Min();
            int maxDay = Array.IndexOf(dailySales, maxSale) + 1;
            int minDay = Array.IndexOf(dailySales, minSale) + 1;

            Console.WriteLine($"\n1. Max Sale: Day {maxDay} = {maxSale}");
            Console.WriteLine($"   Min Sale: Day {minDay} = {minSale}");

            // 2. میانگین فروش
            double averageSale = dailySales.Average();
            Console.WriteLine($"\n2. Average Sale: {averageSale:F2}");

            // 3. روزهایی با فروش بالاتر از میانگین
            var aboveAverageDays = dailySales
                .Select((sale, index) => new { Day = index + 1, Sale = sale })
                .Where(x => x.Sale > averageSale)
                .ToList();

            Console.WriteLine($"\n3. Days with above average sales:");
            foreach (var day in aboveAverageDays)
            {
                Console.WriteLine($"   Day {day.Day}: {day.Sale} (+{day.Sale - averageSale:F1})");
            }

            // 4. رشد/کاهش روزانه
            Console.WriteLine("\n4. Daily Change:");
            for (int i = 1; i < dailySales.Length; i++)
            {
                int change = dailySales[i] - dailySales[i - 1];
                string trend = change > 0 ? "📈" : change < 0 ? "📉" : "➡️";
                Console.WriteLine($"   Day {i + 1}: {dailySales[i]} ({trend} {Math.Abs(change)})");
            }

            // 5. بهترین دوره ۳ روزه متوالی
            int bestThreeDaySum = 0;
            int bestStartDay = 0;

            for (int i = 0; i <= dailySales.Length - 3; i++)
            {
                int threeDaySum = dailySales[i] + dailySales[i + 1] + dailySales[i + 2];
                if (threeDaySum > bestThreeDaySum)
                {
                    bestThreeDaySum = threeDaySum;
                    bestStartDay = i;
                }
            }

            Console.WriteLine($"\n5. Best 3-day period:");
            Console.WriteLine($"   Days {bestStartDay + 1}-{bestStartDay + 3}: " +
                             $"{dailySales[bestStartDay]} + {dailySales[bestStartDay + 1]} + {dailySales[bestStartDay + 2]} = {bestThreeDaySum}");

            // 6. آمار اضافی
            int totalSales = dailySales.Sum();
            int daysAbove100 = dailySales.Count(s => s > 100);
            double percentageAboveAverage = (double)aboveAverageDays.Count / dailySales.Length * 100;

            Console.WriteLine($"\n6. Additional Statistics:");
            Console.WriteLine($"   Total Sales: {totalSales}");
            Console.WriteLine($"   Days with sales > 100: {daysAbove100}");
            Console.WriteLine($"   Percentage of days above average: {percentageAboveAverage:F1}%");
        }
    }
}
