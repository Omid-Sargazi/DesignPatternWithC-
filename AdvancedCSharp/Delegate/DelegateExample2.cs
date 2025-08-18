using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCSharp.Delegate
{
        public delegate void MathOperation(int a, int b);
    public class DelegateExample2
    {

        public static void Add(int a, int b)
        {
           Console.WriteLine(a + b);
        }

        public static void Subtract(int a, int b)
        {
            if(a== 0)
            {
                throw new Exception("Zero Divided is not impossible");
            }
            Console.WriteLine(a/b);
        }


        public static void Minus(int a, int b)
        {
            Console.WriteLine(a - b);
        }
    }
}
