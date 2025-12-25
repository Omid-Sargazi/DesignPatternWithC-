using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQProblemsInCSharp.LinqProblems2
{
    public class Linq1Problem
    {
        public static void Execute()
        {
            int[] numbers = { 5, 2, 9, 8, 3, 6, 1, 4, 7 };

            var result1 = numbers.Where(n => n % 2 == 0).OrderBy(n => n);


            foreach (var res in result1)
            {
                Console.WriteLine($"{res}");
            }

            List<string> students = new List<string>
            {
                "Ali", "Mohammad", "Ahmad", "Sara", "Amir", "Fatemeh", "Armin"
            };

            var result2 = students.
                Where(s => s.StartsWith("A", StringComparison.OrdinalIgnoreCase)).OrderBy(s => s);
            Console.WriteLine(string.Join(", ",result2));

            var products = new[]
            {
                new {Name="laptop",Price=2500},
                new {Name="Mouse",Price=50},
                new {Name="Keyboard",Price=200},
                new {Name="Monitor",Price=1200},
                new {Name="Headphones",Price=300},
            };

            var result3 = products.Where(p => p.Price > 1000)
                .OrderByDescending((p => p))
                .Select(p => $"{p.Name},{p.Price}");
            Console.WriteLine(string.Join(", ", result3));

        }
    }
}
