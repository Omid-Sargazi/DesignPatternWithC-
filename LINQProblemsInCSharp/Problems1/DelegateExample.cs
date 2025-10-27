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
}
