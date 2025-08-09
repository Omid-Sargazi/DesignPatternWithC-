using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.SingletonPattern
{
    public sealed class Logger
    {
        private readonly Lazy<Logger> _logger = new(()=>new Logger());
        private Logger()
        {
            Console.WriteLine("Initi Logger");
        }
    }
}
