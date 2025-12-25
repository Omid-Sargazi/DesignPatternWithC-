using LINQProblemsInCSharp.LinqProblems2;
using LINQProblemsInCSharp.Problems1;

namespace LINQProblemsInCSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            //DelegateExample example = new DelegateExample();
            //example.Run(3,2);

            //DelegateExample2 delegateExample2 = new DelegateExample2();
            //delegateExample2.Run(7,2,(x,y)=>x);

            Linq1Problem.Execute();
        }
    }
}
