using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQProblems.Problems01
{
    public delegate void EventNotificationHandler(object sender,string message);
   

    public class EventManager //Subject or Publisher
    {
        public event EventNotificationHandler OnEvent;

        public void Subscribe(EventNotificationHandler handler) => OnEvent += handler;
        public void UnSubscribe(EventNotificationHandler handler)=>OnEvent-=handler;
        public void RaiseEvent(string message)
        {

        }
    }


    public delegate void SimpleDelegate(string message);

    public class Problem2
    {
        public  void DisplayMessage(string txt)
        {
            Console.WriteLine($"Message:{txt}");
        }


        public void ShowWarning(string warning)
        {
            Console.WriteLine($"Warning: {warning}");
        }


        public void Run()
        {
            Console.WriteLine("Delegate");

            SimpleDelegate myDelegate;

            myDelegate = DisplayMessage;
            myDelegate("Hiiiii");

            myDelegate = ShowWarning;
            myDelegate("This is a warning");

            Console.WriteLine("This completed");
        }


    }


}
