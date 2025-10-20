using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionsInCSharp1.Problems
{
    public interface A
    {
        void MethodA();
    }
    public interface B
    {}
    public interface E
    { }
    public class C:A
    {
        public void MethodA()
        {
            Console.WriteLine("Here is Class C");
        }
    }
    public class D :A,B
    {
        public void MethodA()
        {
            Console.WriteLine("Here is Class D");
        }
    }

    public class F : A,B,E
    {
        public void MethodA()
        {
            Console.WriteLine("Here is Class F");
        }
    }

    public class ClientReflection
    {
        public static void Run(object obj)
        {
           Type type =  obj.GetType();
           Console.WriteLine(type.IsClass);

           var interfaces = type.GetInterfaces();

           foreach (var i in interfaces)
           {
               Console.WriteLine(i.Name);
           }

        }
    }
}
