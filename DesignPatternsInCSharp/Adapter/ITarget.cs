using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsInCSharp.Adapter
{
    public interface ITarget
    {
        string GetRequest();
    }

    public class Adaptee
    {
        public string GetOldRequest()
        {
            return "Old Request from Old System";
        }
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
           return _adaptee.GetOldRequest();
        }
    }
}
