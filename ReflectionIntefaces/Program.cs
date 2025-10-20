using System.Reflection;

namespace ReflectionIntefaces
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var interfaces = typeof(Test).GetInterfaces();

            //interfaces = interfaces.Except(interfaces.SelectMany(x => x.GetInterfaces())).ToArray();

            var totalTypes = Assembly.GetExecutingAssembly().GetTypes();

            var i = 0;

            var HasCInterface = totalTypes.Where(x => x.GetInterfaces().Any(a => a.Name == typeof(A).Name)).ToList();

            i++;

        }
    }

    public interface A : B
    {

    }
    public interface B
    {

    }
    public interface C
    {

    }

    public abstract class ABS : A
    {

    }

    public class Test : ABS
    {

    }

    public class Person : Test, C
    {

    }
}
