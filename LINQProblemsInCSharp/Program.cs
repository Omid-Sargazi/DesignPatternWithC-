using LINQProblemsInCSharp.Problems1;

namespace LINQProblemsInCSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            DelegateExample example = new DelegateExample();
            example.Run(3,2);
        }
    }
}
