using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsWithCSharp.ChainOfResponsibility
{
    public abstract class SupportHandler
    {
        protected SupportHandler _next;
        public void SetNext(SupportHandler next)
        {
            _next = next;
        }
        public abstract void Handle(string requestType);
    }

    public class Level1Support : SupportHandler
    {
        public override void Handle(string requestType)
        {
            if (requestType == "General")
                Console.WriteLine("Level1 handle the request");
            else if(_next != null)
                _next.Handle(requestType);
        }
    }

    public class TechnicalSupport : SupportHandler
    {
        public override void Handle(string requestType)
        {
            if (requestType == "Technical")
                Console.WriteLine("Technical Engineer handled the request.");
            else if (requestType == "Technical")
                _next.Handle(requestType);
        }
    }

    public class ManagerSupport : SupportHandler
    {
        public override void Handle(string requestType)
        {
            if (requestType == "complain")
                Console.WriteLine("manager handle the request.");
            else Console.WriteLine("Unlnown request. NO handler could process it.");
        }
    }
}
