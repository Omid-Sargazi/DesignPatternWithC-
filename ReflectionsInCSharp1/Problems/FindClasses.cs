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
        public void ExtraMethod() => Console.WriteLine("Extra in C");
    }
    public class D :A,B
    {
        public void MethodA()
        {
            Console.WriteLine("Here is Class D");
        }
        public void AnotherMethod() => Console.WriteLine("Another in D");
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
           Console.WriteLine($"Is class? {type.IsClass}");

            var interfaces = type.GetInterfaces();


           var assembly = Assembly.GetExecutingAssembly();


            foreach (var i in interfaces)
           {
               Console.WriteLine($"\nInterface: {i.Name}");

                var interfaceType = i;

               var implementations = assembly.GetTypes()
                   .Where(t => interfaceType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                   .ToList();

               foreach (var impl in implementations)
               {
                   Console.WriteLine($"  -> {impl.Name}");
                   var instance = Activator.CreateInstance(impl);
                   var methods = impl.GetMethods();
                   foreach (var method in methods)
                   {
                       if (method.Name.StartsWith("Method") && method.GetParameters().Length == 0)
                       {
                           Console.WriteLine($" -> Executing {method.Name}");
                           method.Invoke(instance, null);
                       }
                   }
               }

            }


        }
    }
}
