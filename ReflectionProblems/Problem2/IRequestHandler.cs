using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionProblems.Problem2
{
    public interface IRequestHandler<TRequest,TResult>
    {
    }

    public class CreateUserCommand{}
    public class GetUserQuery{}

    public class CreateUserHandler : IRequestHandler<CreateUserCommand,bool>{}
    public class GetUserHandler:IRequestHandler<GetUserQuery,string>{}
    public class Logger{}

    public class CLienHandler
    {
        private static Dictionary<Type, Type> _handlerType = new Dictionary<Type, Type>();
        public static void Run()
        {
            RegisterHandlers(Assembly.GetExecutingAssembly());
            Console.WriteLine("Map request to Handler");

            foreach (var mapping in _handlerType)
            {
                Console.WriteLine($"{mapping.Key.Name}->{mapping.Value.Name}");
            }
        }

        private static void RegisterHandlers(Assembly assembly)
        {
            var handlerTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))).ToList();

            Console.WriteLine($"Founded Handlers: {handlerTypes.Count}");

            foreach (var handlerType in handlerTypes)
            {
                Console.WriteLine($" process handler{handlerType.Name}");

                var interfaceType = handlerType.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));
                Console.WriteLine($"  اینترفیس: {interfaceType.Name}");
                var requestType = interfaceType.GetGenericArguments()[0];
                Console.WriteLine($"  تایپ درخواست: {requestType.Name}");

                _handlerType[requestType] = handlerType;

                Console.WriteLine($"  ✅ ثبت شد: {requestType.Name} → {handlerType.Name}");
            }
        }
    }
}
