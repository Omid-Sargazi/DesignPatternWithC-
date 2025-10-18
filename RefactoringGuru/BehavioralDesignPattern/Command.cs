using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringGuru.BehavioralDesignPattern
{
    public interface ICommand
    {
        void Execute();
    }

    public class SimpleCommand : ICommand
    {
        private string _payload = string.Empty;

        public  SimpleCommand(string payload)
        {
            _payload = payload;
        }

        public void Execute()
        {
            Console.WriteLine($"SimpleCommand: See, I can do simple things like printing ({this._payload})");
        }
    }

    public class ComplexCommand:ICommand
    {
        private Receiver _receiver;
        private string _a;
        private string _b;

        public ComplexCommand(Receiver receiver, string a, string b)
        {
            _a = a;
            _b = b;
            _receiver = receiver;
        }
        public void Execute()
        {
            Console.WriteLine("ComplexCommand: Complex stuff should be done by a receiver.");
            _receiver.DoSomething(_a);
            _receiver.DoSomethingElse(_b);
        }
    }

    public class Receiver
    {
        public void DoSomething(string a)
        {
            Console.WriteLine($"Receiver: Working on ({a}.)");
        }

        public void DoSomethingElse(string b)
        {
            Console.WriteLine($"Receiver: Also working on ({b}.)");
        }
    }

    public class Invoker
    {
        private ICommand _onStart;
        private ICommand _onEnd;

        public void SetOnStart(ICommand command)
        {
            _onStart = command;
        }
        public void SetOnFinish(ICommand command)
        {
            _onEnd = command;
        }

        public void DoSomethingImportant()
        {
            Console.WriteLine("Invoker: Does anybody want something done before I begin?");
            if (_onStart is ICommand)
            {
                _onStart.Execute();
            }
            Console.WriteLine("Invoker: ...doing something really important...");
            Console.WriteLine("Invoker: Does anybody want something done after I finish?");
            if (_onEnd is ICommand)
            {
                _onEnd.Execute();
            }
        }

        public class CLientCommandPattern
        {
            public static void Run()
            {
                Invoker invoker = new Invoker();
                invoker.SetOnStart(new SimpleCommand("Say Hi"));
                Receiver receiver = new Receiver();
                invoker.SetOnFinish(new ComplexCommand(receiver, "send email", "save report"));
                invoker.DoSomethingImportant();
            }

        }
    }
}
