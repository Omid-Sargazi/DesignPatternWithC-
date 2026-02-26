using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatternsInCSharp.Bridge;

namespace DesignPatternsInCSharp.Composition
{
    public interface IOrderStep
    {
        void Execute(Order order);
    }

    public class OrderPipeline
    {
        private readonly List<IOrderStep> _steps = new();

        public void AddStep(IOrderStep step) => _steps.Add(step);

        public void Process(Order order)
        {
            foreach (var step in _steps)
                step.Execute(order);
        }
    }

    public class ValidateOrderStep : IOrderStep
    {
        public void Execute(Order order) => Console.WriteLine("Validating order...");
    }

}
