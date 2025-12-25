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
        }
    }
}
