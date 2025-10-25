using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAndDesignPattern.BehavioralDesignPattern.ChainOfResponsibility
{
    public class UserRequest
    {
        public int UserId { get; set; }
        public string Role { get; set; }
        public bool Permissions { get; set; }
    }

    public interface IHandlerr
    {
        IHandlerr SetNext(IHandlerr next);
        void Handle(UserRequest request);
    }

    public abstract class HandlerBase : IHandlerr
    {
        private IHandlerr _next;
        public IHandlerr SetNext(IHandlerr next)
        {
            _next = next;
            return this;
        }

        protected virtual void PassToNext(UserRequest request)
        {
            _next?.Handle(request);
        }

        public abstract void Handle(UserRequest request);
    }

    public class AuthenticationHandler : HandlerBase
    {
        public override void Handle(UserRequest request)
        {
            if (request.Role == "Auth")
            {
                Console.WriteLine("Authentication");
            }
            else
            {
                PassToNext(request);
            }
        }
    }

    public class AuthorizationHandler:HandlerBase
    {
        public override void Handle(UserRequest request)
        {
            if (request.Role == "Authorization")
            {
                Console.WriteLine("Authorization");
            }
            else
            {
                PassToNext(request);
            }
        }
    }

    public class ValidationHandler : HandlerBase
    {
        public override void Handle(UserRequest request)
        {
            if (request.Role == "Valid")
            {
                Console.WriteLine("Validation");
            }
            else
            {
                PassToNext(request);
            }
        }
    }
}
