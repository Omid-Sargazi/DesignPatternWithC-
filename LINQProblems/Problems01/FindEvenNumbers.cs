using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LINQProblems.Problems01
{
    public class FindEvenNumbers
    {
        public static void Run()
        {
            var nums1 = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var res1 = nums1.Where(n => n % 2 == 0).ToList();

            var names = new List<string> { "ali", "bahar", "reza", "narges" };
            var res2 = names.Select(n => n.ToUpper()).ToList();

            var res3 = nums1.Sum();

            var people = new List<Person>
            {
                new Person { Name = "Omid", Age = 43 },
                new Person { Name = "Saeed", Age = 39 },
                new Person { Name = "Saleh", Age = 15 },
                new Person { Name = "Karim", Age = 55 },
            };

            var res4 = people.Max(p => p.Age);


            var res5 = people.GroupBy(p => p.Name[0]);
            foreach (var group in res5)
            {
                Console.WriteLine($"Group:{group.Key}");
                foreach (var person in group)
                {
                    Console.WriteLine($"--{person.Name}");
                }
            }


            var res6 = people.OrderByDescending(p => p.Age).ToList();
            var res7 = people.FirstOrDefault(p=>p.Age>25);
            if (res7 != null)
            {
                Console.WriteLine($"{res7.Name},{res7.Age}");
            }

            var res8 = people.Any(p => p.Name == "Bahar");

            var res9 = people.Select(p => new { Name = p.Name, Age = p.Age });

            var res10 = people.Average(p => p.Age);

            var products = new List<Product>
            {
                new Product { Name = "لپ‌تاپ", Category = "الکترونیک", Price = 450 },
                new Product { Name = "ماوس", Category = "الکترونیک", Price = 50 },
                new Product { Name = "کتاب", Category = "آموزشی", Price = 30 },
                new Product { Name = "هدفون", Category = "الکترونیک", Price = 150 },
                new Product { Name = "تلویزیون", Category = "الکترونیک", Price = 800 }
            };

        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } // ممکن است null باشد
    }


}
