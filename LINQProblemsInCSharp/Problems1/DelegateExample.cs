using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQProblemsInCSharp.Problems1
{

    public delegate int Calculator(int a, int b);
    public class DelegateExample
    {
        public void Run(int a, int b)
        {
            Calculator calculator = Add;
            Console.WriteLine(calculator(a,b));
            calculator += Mul;
            Console.WriteLine(calculator(a,b));
        }
        public int Add(int a, int b) => a + b;
        public int Mul(int a, int b) => a * b;
    }

    public class DelegateExample2
    {
        public void Run(int a, int b, Func<int,int,int> cal)
        {
            Console.WriteLine("Original delegate: " + cal(a, b));

            Console.WriteLine("Add: " + Add(a, b));
            Console.WriteLine("Mul: " + Mul(a, b));
            cal = Add;
            Console.WriteLine(cal(a,b));
            cal += Mul;
            Console.WriteLine(cal(a,b));
        }

        public int Add(int a, int b) => a + b;
        public int Mul(int a, int b) => a * b;
    }
}
