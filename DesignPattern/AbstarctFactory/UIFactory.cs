using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.AbstarctFactory
{
    public interface IButton
    {
        void Click();
    }

    public interface ITextBox
    {
        void WriteText();
    }

    public interface ICheckBox
    {
        void Click();
    }

    public class WinButton : IButton
    {
        public void Click()
        {
            Console.WriteLine("Window CLick on button");
        }
    }

    public class MacButton : IButton
    {
        public void Click()
        {
            Console.WriteLine("Mac CLick on button");
        }
    }

    public class WinTextBox : ITextBox
    {
        public void WriteText()
        {
           Console.WriteLine("text box for win");
        }
    }

    public class WinCheckBox : ICheckBox
    {
        public void Click()
        {
            Console.WriteLine("Check box for win");
        }
    }

    public class MacTextbox : ITextBox
    {
        public void WriteText()
        {
            Console.WriteLine("Text box for Mac");

        }
    }

    public class MacCheckBox : ICheckBox
    {
        public void Click()
        {
            Console.WriteLine("Check box for Mac");
        }
    }

    public interface IUIFactory
    {
        IButton CreateButton();
        ICheckBox CreateCheckBox();
        ITextBox CreateTextBox();
    }
   

    public class WinUIFactory : IUIFactory
    {
        public IButton CreateButton()
        {
           return new WinButton();
        }

        public ICheckBox CreateCheckBox()
        {
            return new WinCheckBox();
        }

        public ITextBox CreateTextBox()
        {
           return new WinTextBox();
        }
    }

    public class MacUIFactory : IUIFactory
    {
        public IButton CreateButton()
        {
            return new MacButton();
        }

        public ICheckBox CreateCheckBox()
        {
            return new MacCheckBox();
        }

        public ITextBox CreateTextBox()
        {
            return new MacTextbox();
        }
    }

    public class ClientUIFactory
    {
        private IButton _button;
        private ICheckBox _checkBox;
        private ITextBox _textBox;
        public ClientUIFactory(IUIFactory factory)
        {
            _button = factory.CreateButton();
            _checkBox = factory.CreateCheckBox();
            _textBox = factory.CreateTextBox();
        }

        public void Run()
        {
            _button.Click();
            _checkBox.Click();
            _textBox.WriteText();
        }
    }
}
