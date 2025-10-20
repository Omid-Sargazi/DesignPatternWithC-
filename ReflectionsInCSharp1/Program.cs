using ReflectionsInCSharp1.Problems;

namespace ReflectionsInCSharp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            C c = new C();
            F f = new F();
            ClientReflection.Run(f);
        }
    }
}
