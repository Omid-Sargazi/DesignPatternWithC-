using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCSharp.Delegate
{
    public class Calculator
    {
        public int Add(int a, int b) => a + b;
        public int Subtract(int a, int b) => a - b;
    }


    public class RunCal
    {
        public static void Run(int a, int b,string MethodName)
        {
            Calculator calc = new Calculator();
            string methodName = MethodName;

            MethodInfo method = typeof(Calculator).GetMethod(methodName);

            Func<int, int, int> operation =(Func<int, int, int>) System.Delegate.CreateDelegate(typeof(Func<int, int, int>), calc, method);

            int resulttt = operation(a, b);
            Console.WriteLine(resulttt);
        }
    }
}
