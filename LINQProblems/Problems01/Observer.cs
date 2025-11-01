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

    public delegate void MultiDelegate(string message);

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

        public static void MethodA(string msg)
        {
            Console.WriteLine($"MethodA: {msg}");
        }

        static void MethodB(string msg)
        {
            Console.WriteLine($"MethodB: {msg.ToUpper()}");
        }

        static void MethodC(string msg)
        {
            Console.WriteLine($"MethodC: {msg} - طول: {msg.Length}");
        }

        static void MethodD(string msg)
        {
            Console.WriteLine($"MethodD: {msg.ToUpper()}");
        }

        static void MethodE(string msg)
        {
            Console.WriteLine($"MethodE: {msg} - طول: {msg.Length}");
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

        public static void Run2()
        {
            Console.WriteLine("=== Multicast Delegate ===");

            MultiDelegate multiDelegate = MethodA;
            multiDelegate += MethodB;
            multiDelegate += MethodC;

            Console.WriteLine("call all methods");

            multiDelegate("Hello Multicast");

            Console.WriteLine("Delete a method");
            multiDelegate -= MethodB;

            Console.WriteLine("after delete a method");

            if (multiDelegate != null)
            {
                Console.WriteLine($"{multiDelegate.GetInvocationList().Length}");
            }
        }


    }


}
