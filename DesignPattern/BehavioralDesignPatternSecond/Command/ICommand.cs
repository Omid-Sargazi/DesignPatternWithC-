using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.BehavioralDesignPatternSecond.Command
{
    internal interface ICommand
    {
        string Name { get; }
        void Execute();
    }

    public class StartCommand:ICommand
    {
        public string Name
        {
            get { return "Start"; }
        }
        public void Execute()
        {
            Console.WriteLine("I am executing StartCommand");
        }
    }

    public class StopCommand:ICommand
    {
        public string Name
        {
            get { return "Stop"; }
        }
        public void Execute()
        {
            Console.WriteLine("I am executing StopCommand");
        }
    }

    public class Invoker
    {
        private readonly Dictionary<string, ICommand> _commands;

        public Invoker()
        {
            _commands = new Dictionary<string, ICommand>
            {
                { "Start", new StartCommand() },
                { "Stop", new StopCommand() }
            };
        }

        public ICommand GetCommand(string action)
        {
            if (_commands.TryGetValue(action, out var command))
            {
                return command;
            }

            // Handle unknown command
            return null;
            // Or throw: throw new ArgumentException($"Unknown command: {action}");
        }
    }
}
