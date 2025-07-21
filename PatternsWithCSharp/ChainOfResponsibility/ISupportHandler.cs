using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsWithCSharp.ChainOfResponsibility
{
    public class SupportRequest
    {
        public string Issue { get; set; }
        public int Severity { get; set; }
    }
    public interface ISupportHandler
    {
        void SetNext(ISupportHandler next);
        void handleRequest(SupportRequest request);
    }

    public abstract class AbstarctSupportHandler : ISupportHandler
    {
        protected ISupportHandler _nextHandler;
        public abstract void handleRequest(SupportRequest request);

        public void SetNext(ISupportHandler next)
        {
           _nextHandler = next;
        }
    }

    public class classLevel1Supporthandler : AbstarctSupportHandler
    {
        public override void handleRequest(SupportRequest request)
        {
           if(request.Severity == 1)
            {
                Console.WriteLine("Level 1 Support: ISssue resolved-"+request.Issue);
            }
           else if (_nextHandler !=null)
            {
                   _nextHandler.handleRequest(request);
            }
           else
                Console.WriteLine("No support level can handle this request.");

        }
    }

    public class Level2SupportHandler : AbstarctSupportHandler
    {
        public override void handleRequest(SupportRequest request)
        {
            if (request.Severity == 2)
                Console.WriteLine("Level 2 Support: Issue resolved - " + request.Issue);
            else if (_nextHandler != null)
                _nextHandler.handleRequest(request);
            else Console.WriteLine("No support level can handle this request.");

        }
    }
}
