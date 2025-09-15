using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCSharp.LINQExamples
{
    public class Example1
    {
        public static void Run()
        {
            int[] nums = new[] { 7, 4, 5, 6, 8, 9, 0, 10, 4, 4, 5, 6, 7, 100 };

            var people = new Person[]
            {
                new Person { Name = "Omid", Age = 42 },
                new Person { Name = "Saeed", Age = 74 },
                new Person { Name = "Vahid", Age = 14 },
                new Person { Name = "Amir", Age = 24 },
            };

            var squares = nums.Select(n => n * n);
            var names = people.Select(p => p.Name);
            var upperName = names.Select(n => n.ToUpper());
            var nameLengths = names.Select(n => new { n, n.Length });
            var index = names.Select((n, ind) => $"{ind}:{n}");

        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
