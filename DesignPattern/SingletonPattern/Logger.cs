using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.SingletonPattern
{
    public sealed class Logger
    {
        private static readonly Lazy<Logger> _instance = new(()=>new Logger());
        private Logger()
        {
            Console.WriteLine("\"Logger initialized.");
        }

        public static Logger Instance => _instance.Value;

        public void Log(string message)
        {
            Console.WriteLine($"[Log]:{message}");
        }


    }
}
