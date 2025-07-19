using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopositionPattern.CompositionPattern
{
    public interface IEmployee
    {
        void Dispaly(int indent=0);
    }

    public class IndividualEmployee : IEmployee
    {
        private readonly string _name;
        public IndividualEmployee(string name)
        {
            _name = name;
        }
        public void Dispaly(int indent = 0)
        {
            Console.WriteLine(new string(' ', indent) + "_" + _name);
        }
    }

    public class Manager : IEmployee
    {
        private readonly string _name;
        protected readonly List<IEmployee> _employees = new();
        public Manager(string name)
        {
            _name=name;
        }
        public void Add(IEmployee employee)
        {
            _employees.Add(employee);
        }
        public void Dispaly(int indent = 0)
        {
            Console.WriteLine(new string(' ', indent) + _name);
            foreach(var employee in _employees)
            {
                employee.Dispaly(indent+2);
            }
        }
    }

    public class DisplayCompanyEmployee
    {
        public static void Run()
        {
            IEmployee Dev1 = new IndividualEmployee("Dev1");
            IEmployee Dev2 = new IndividualEmployee("Dev2");
            IEmployee Dev3 = new IndividualEmployee("Dev3");
            IEmployee Dev4 = new IndividualEmployee("Dev4");
            IEmployee Dev5 = new IndividualEmployee("Dev5");

            Manager CTO = new Manager("CTO");
            Manager CFO = new Manager("CFO");
            Manager CEO = new Manager("CEO");

            CTO.Add(Dev1);
            CTO.Add(Dev2);
            CFO.Add(Dev3);
            CEO.Add(CTO);
            CEO.Add(CFO);
            CEO.Add(Dev4);
            CEO.Add(Dev5);

            CEO.Dispaly();

        }
    }
}
