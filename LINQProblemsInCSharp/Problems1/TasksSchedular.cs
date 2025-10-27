using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQProblemsInCSharp.Problems1
{
    public interface ITasks
    {
        void Execute();
    }
    public class Tasks:ITasks
    {
        public void Execute()
        {
            Console.WriteLine($"Execute a Task.{DateTime.Now}");
        }
    }

    public class TaskSchedular
    {
        //private List<ITasks> _tasks = new List<ITasks>();
        private readonly ITasks _tasks;

        public TaskSchedular(ITasks tasks)
        {
            //_tasks.Add(tasks);
            _tasks = tasks;
        }

        public void ExecuteAt10()
        {
            while (true)
            {
                int h = DateTime.Now.Hour;
                int m = DateTime.Now.Minute;
                int s = DateTime.Now.Second;

                if (h == 10 && m == 0 && s == 0)
                {
                    _tasks.Execute();
                    break;
                }

                Thread.Sleep(1000);
            }
        }
    }
}
