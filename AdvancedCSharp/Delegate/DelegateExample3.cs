using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCSharp.Delegate
{
    public delegate bool NumberChecker(int number);
    public class DelegateExample3
    {
        public static void CheckNumbers(int[] numbers, NumberChecker checker)
        {
            foreach(var num in numbers)
            {
                if(checker(num))
                {
                    Console.WriteLine($"{num} has critrion.");
                }
            }
        }

        public static bool IsEvent(int num) => num % 2 == 0;
        public static bool IsPositive(int num) => num > 0;
    }
}
