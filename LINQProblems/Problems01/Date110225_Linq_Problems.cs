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


        }
    }


    public class Student
    {
        public string Name { get; set; }
        public List<string> Courses { get; set; }
    }




}
