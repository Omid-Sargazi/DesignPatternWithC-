using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionProblems
{
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public interface IHandler<T>{}
    public interface INotification{}

    public class UserHandler:IHandler<User>{}
    public class ProductHandler : IHandler<int>{}
    public class EmailServices : INotification{}

    public interface ICommandHandler{}
    public interface IUser{}
    public interface IQueryHandler<T>{}

    public class CreateUserHandler : ICommandHandler,IUser
    {}
    public class GetUserHandler : IQueryHandler<string>{}
    public class EmailService{}

    public class ClientUser
    {
        public static void Run()
        {

            var assembly = Assembly.GetExecutingAssembly();
            var genericInterfaceTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType));
            Console.WriteLine("types  that have Generic Types");
            foreach (var type in genericInterfaceTypes)
            {
                foreach (var interfaceType in type.GetInterfaces().Where(i=>i.IsGenericType))
                {
                    Console.WriteLine($"{type.Name} {interfaceType.Name}");
                    Console.WriteLine($"Interface Base==> {interfaceType.GetGenericTypeDefinition().Name}");
                    Console.WriteLine($" Argument Type==> {interfaceType.GetGenericArguments()[0]}");
                }
            }



            var allTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any());

            Console.WriteLine($"Types that implement at least one interfaces.");

            Console.WriteLine("All types that are in Assembly.");
            //foreach (var type in allTypes)
            //{
            //    var interfaces = string.Join(",", type.GetInterfaces().Select(i => i.Name));
            //    Console.WriteLine($"{type.Name} : {interfaces}");
            //    //Console.WriteLine($"{type.Name}.");

            //}

        }
    }
}
