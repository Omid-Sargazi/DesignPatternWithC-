using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAndDesignPattern.BehavioralDesignPattern.ChainOfResponsibility
{
    public enum ReqType
    {
        SimpleReq = 1,
        TechnicalReq = 2,
        ComplexReq = 3,
        FinancialReq = 4
    };

    public interface IRequestHandler
    {
        IRequestHandler Handle(ReqType request,IRequestHandler  req);
    }

    public abstract class RequestHandler : IRequestHandler
    {
        private IRequestHandler _nextHandler;
        public IRequestHandler Handle(ReqType request, IRequestHandler req)
        {
            throw new NotImplementedException();
        }
    }



   
}
