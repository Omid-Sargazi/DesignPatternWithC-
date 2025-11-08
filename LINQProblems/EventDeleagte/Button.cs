using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQProblems.EventDeleagte
{
    public class Button
    {
        public event Action<string> OnClick;
        private string _buttonName;

        public Button(string name)
        {
            _buttonName = name;
        }

        public void Click()
        {
            Console.WriteLine($"Clicked Button.{_buttonName}");
            OnClick?.Invoke(_buttonName);
        }
    }

    public class ButtonHandler
    {
        static void HandleButtonClick(string buttonName)
        {
            Console.WriteLine($"clicked on button {buttonName}");
        }

        static void LogButtonClick(string buttonName)
        {
            Console.WriteLine($"get logged on button {buttonName}");
        }

        public static void Execute()
        {
            Console.WriteLine("Base Event");

            Button myButton = new Button("Save");
            myButton.OnClick += HandleButtonClick;
            myButton.OnClick += LogButtonClick;
            myButton.Click();

            Console.WriteLine($"Add new handler.");

            myButton.OnClick += (name) =>
            {
                Console.WriteLine($"Lambda button clicked{name}");
            };
            myButton.Click();

            Console.WriteLine("Delete a Handler");
            myButton.OnClick -= LogButtonClick;
            myButton.Click();
        }
    }
}
