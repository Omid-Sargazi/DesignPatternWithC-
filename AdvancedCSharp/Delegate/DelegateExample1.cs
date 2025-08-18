using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCSharp.Delegate
{
    public class DelegateExample1
    {
        public delegate void ShowMessage(string message);

            ShowMessage messageDelegate = DisplayMessage;
        public static void Run()
        {
            
        }

        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
