using System.Reflection;

namespace ReflectionsInCSharp.Problem01
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

    public class AB:A,B
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

    public class BC:B,C
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

    public class AssemblyInterfaceMapper
    {
        public void Execute(Assembly assembly)
        {
            var types = assembly.GetTypes();

            var interfaces = types.Where(t => t.IsInterface);
            var classes = types.Where(t => t.IsClass && !t.IsAbstract);



            foreach (var type in types)
            {
                Console.WriteLine(type);
            }
        }
    }

    public class InterfaceRegistry
    {
        public Dictionary<Type, List<Type>> InterfaceToClassMap { get; } = new();

        public void Register<TInterface, TImplementation>() where TImplementation:TInterface
        {
            var interfaceType = typeof(TInterface);
            var implementationType = typeof(TImplementation);

            if(!InterfaceToClassMap.ContainsKey(interfaceType))
                InterfaceToClassMap[interfaceType] = new List<Type>();
            InterfaceToClassMap[interfaceType].Add(implementationType);

        }

        public void DisplayRegistry()
        {
            foreach (var (interfaceType,implementations) in InterfaceToClassMap)
            {
                Console.WriteLine($"Interfaces:{interfaceType.Name}");

                foreach (var impl in implementations)
                {
                   Console.WriteLine($"Implemeting class:{impl.Name}"); 
                }
            }
        }
    }
}
