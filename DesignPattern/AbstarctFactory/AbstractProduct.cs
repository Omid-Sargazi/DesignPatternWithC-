using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.AbstarctFactory
{
    public interface IAbstractProductA
    {
        string UsefulFunctionA();
    }
    public interface IAbstarctProductB
    {
        string UsefulFunctionB();
    }

    public interface IAbstractProductC
    {
        string UsefulFunctionC();
    }

    public class ConcreateProductA1 : IAbstractProductA
    {
        public string UsefulFunctionA()
        {
            return "The result of the product A1.";
        }
    }

    public class ConcreateProductA2 : IAbstractProductA
    {
        public string UsefulFunctionA()
        {
            return "The result of the product A2";
        }
    }

    public class ConcreateProductA3 : IAbstractProductA
    {
        public string UsefulFunctionA()
        {
            return "The result of the product A3";
        }
    }

    public class ConcreateProductB1 : IAbstarctProductB
    {
        public string UsefulFunctionB()
        {
            return "The result of the prodcut B1";
        }
    }

    public class ConcreateProductB2 : IAbstarctProductB
    {
        public string UsefulFunctionB()
        {
            return "The result of the prodcut B2";
        }
    }

    public class ConcreateProductB3 : IAbstarctProductB
    {
        public string UsefulFunctionB()
        {
            return "The result of the prodcut B3";
        }
    }

    public class ConcreateProductC1 : IAbstractProductC
    {
        public string UsefulFunctionC()
        {
            return "The result of the product C1";
        }
    }

    public class ConcreateProductC2 : IAbstractProductC
    {
        public string UsefulFunctionC()
        {
            return "The result of the product C2";

        }
    }

    public class ConcreateProductC3 : IAbstractProductC
    {
        public string UsefulFunctionC()
        {
            return "The result of the product C3";

        }
    }

    public interface IAbstarctFactory
    {
        IAbstractProductA CreateProductA();
        IAbstarctProductB CreateProductB();
        IAbstractProductC CreateProductC();
    }

    
}
