using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionProblems.RerlectionsPronlems
{
    public interface A
    {
        void MethodA();
    }

    public interface B
    {
        void MethodB();
    }

    public interface C
    {
        void MethodC();
    }

    public class AB : A, B
    {
        public void MethodA()
        {
            Console.WriteLine("There is AB Class and MethodA ");
        }

        public void MethodB()
        {
            Console.WriteLine("There is AB Class and MethodB");
        }
    }

    public class BC : B, C
    {
        public void MethodB()
        {
            Console.WriteLine("There is BC Class and MethodB ");
        }

        public void MethodC()
        {
            Console.WriteLine("There is BC Class and MethodC ");
        }
    }

    public class AssemblyInterface
    {
        public IEnumerable<Type> FindAllInterfaces(Assembly assembly)
        {
            var types = assembly.GetTypes();
            var interfaces = types.Where(t => t.IsInterface);
            return interfaces;
        }

        public IEnumerable<Type> FindAllInterfaces()
        {
            return FindAllInterfaces(Assembly.GetExecutingAssembly());
        }
    }
}
