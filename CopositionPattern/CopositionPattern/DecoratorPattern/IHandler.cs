using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopositionPattern.DecoratorPattern
{
    public interface IHandler
    {
        void Handle(string req);
    }

    public class BasicHandler : IHandler
    {
        public void Handle(string req)
        {
            Console.WriteLine("✅ Request processed");
        }
    }

    public class LoggingHandler : IHandler
    {
        private readonly IHandler _handler;
        public LoggingHandler(IHandler handler)
        {
            _handler = handler;
        }
        public void Handle(string req)
        {
            Console.WriteLine($"[LOG] Handling request: {req}");
            _handler.Handle(req);
        }
    }

    public class AuthenticationHandler : IHandler
    {
        private readonly IHandler _handler;
        public AuthenticationHandler(IHandler handler)
        {
            _handler= handler;
        }
        public void Handle(string req)
        {
            Console.WriteLine("[AUTH] User authenticated.");
            _handler.Handle(req);
        }
    }

    public class AuthorizationHandler : IHandler
    {
        private readonly IHandler _handler;
        public AuthorizationHandler(IHandler handler)
        {

            _handler= handler;
        }

        public void Handle(string req)
        {
            Console.WriteLine("[AUTHZ] User authorized.");
            _handler.Handle(req);
        }
    }

    public class WebRequestHandler
    {
        public void Run()
        {
            IHandler handler = new BasicHandler();
            handler = new AuthenticationHandler(handler);
            handler = new LoggingHandler(handler);
            handler = new AuthorizationHandler(handler);

            handler.Handle("GET/admin/dashboard");
        }
    }
}
