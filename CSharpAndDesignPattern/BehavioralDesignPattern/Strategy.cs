using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAndDesignPattern.BehavioralDesignPattern
{
    internal interface ICacheRepository
    {
        string Get(string ke);
    }

    public class RedisCacheRepository:ICacheRepository
    {
        public string Get(string ke)
        {
            return "Get value from RedisCache";
        }
    }

    public class InMemoryCacheRepository : ICacheRepository
    {
        public string Get(string ke)
        {
            return "Get value from InMemoryCache";
        }
    }
}
