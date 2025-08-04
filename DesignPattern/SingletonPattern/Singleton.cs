using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.SingletonPattern
{
    public class Singleton
    {
        private static volatile Singleton instance;
        private static object lockObject = new object();
        private Singleton() { }

        public static Singleton Instance { get
            {
                if(instance==null)
                {
                    lock(lockObject)
                    {
                        if(instance == null)
                        {
                            instance = new Singleton();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
