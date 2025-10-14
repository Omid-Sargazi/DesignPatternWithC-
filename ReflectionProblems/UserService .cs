using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionProblems
{
    public class UserService
    {
    }
    public class ProductService{}
    public interface IRepository{}

    public class ClientRef
    {
        public static void Run()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var allTypes = assembly.GetTypes();

            Console.WriteLine("All types that are in Assembly.");
            foreach (var type in allTypes)
            {
                Console.WriteLine($"--{type.Name}");
            }
        }
    }
}
