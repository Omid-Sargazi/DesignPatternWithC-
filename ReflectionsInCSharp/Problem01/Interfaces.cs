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

        }
    }
}
