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



            var students = new List<Student>
            {
                new Student { Name = "علی", Courses = new List<string> { "ریاضی", "فیزیک", "شیمی" } },
                new Student { Name = "سمیرا", Courses = new List<string> { "ریاضی", "ادبیات" } },
                new Student { Name = "رضا", Courses = new List<string> { "فیزیک", "برنامه‌نویسی" } }
            };

            var allCourses = students.SelectMany(s => s.Courses)
                .Distinct().ToList();


            var products2 = new List<Product>
            {
                new Product { Name = "لپ‌تاپ", Category = "الکترونیک", Price = 450 },
                new Product { Name = "ماوس", Category = "الکترونیک", Price = 50 },
                new Product { Name = "کتاب", Category = "آموزشی", Price = 30 },
                new Product { Name = "هدفون", Category = "الکترونیک", Price = 150 },
                new Product { Name = "تلویزیون", Category = "الکترونیک", Price = 800 }
            };

            var res = products2.GroupBy(p => p.Category).Select(g => new
            {
                Category = g.Key,
                ExpensiveProducts = g.Where(p => p.Price > 100).ToList(),
                TotalExpensiveProducts = g.Count(p => p.Price > 100)
            }).Where(g => g.ExpensiveProducts.Any()).ToList();

            var customers = new List<Customer>
            {
                new Customer { Name = "علی", City = "تهران" },
                new Customer { Name = "بهرام", City = "مشهد" },
                new Customer { Name = "سمیرا", City = "تهران" },
                new Customer { Name = "نرگس", City = "اصفهان" },
                new Customer { Name = "رضا", City = "مشهد" }
            };

            var res2 = customers.Select(c => c.City).Distinct().ToList();

            var res3 = products.OrderBy(p => p.Category)
                .ThenByDescending(p => p.Price).ToList();




        }
    }


    public class Student
    {
        public string Name { get; set; }
        public List<string> Courses { get; set; }
    }

    public class  Customer
    {
        public string Name { get; set; }
        public string City { get; set; }
    }




}
