using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.Patterns
{
    public class Adaptee
    {
        public string getSpecificRequest()
        {
            return "Specific Request";
        }
    }

    public interface ITarget
    {
        public string GetRequest();
    }
    public class Adapter : ITarget
    {
        private readonly Adaptee _adaptee;

        public Adapter(Adaptee adaptee)
        {
            _adaptee = adaptee;
        }
        public string GetRequest()
        {
            return $"This is {_adaptee.getSpecificRequest()}";
        }
    }

    public class AdapterClient
    {
        public static void Run()
        {
            Adaptee adaptee = new Adaptee();
            Adapter adapter = new Adapter(adaptee);
            Console.WriteLine(adapter.GetRequest());
        }
    }
}
