using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAndLeetCode.LINQProject
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        object Handle(object request);
    }

    public class AbstractHandler:IHandler
    {
        private IHandler _handler;
        public IHandler SetNext(IHandler handler)
        {
            _handler = handler;

            return handler;
        }

        public virtual object Handle(object request)
        {
            if (_handler != null)
            {
               return _handler.Handle(request);
            }
            else
            {
                return null;
            }
        }

        public class MonkeyHandler : AbstractHandler
        {
            public override object Handle(object request)
            {
                if ((request as string) == "Banaan")
                {
                    return $"Monkey: I'll eat the {request.ToString()}.\n";
                }
                else
                {
                    return base.Handle(request);
                }
            }
        }
    }
}
