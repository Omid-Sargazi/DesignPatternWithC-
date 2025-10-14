using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAndDesignPattern.CreationalDesignPattern
{
    public interface IButton
    {
        void CreateButton();
    }

    public interface ITextBox
    {
        void CreateTextBox();
    }

    public class WinButton : IButton
    {
        public void CreateButton()
        {
            Console.WriteLine($"Create a Button for Win ");
        }
    }

    public class WinTextBox:ITextBox
    {
        public void CreateTextBox()
        {
            Console.WriteLine($"Create a TextBox for Win ");

        }
    }

    public class MacButton : IButton
    {
        public void CreateButton()
        {
            Console.WriteLine($"Create a Button for Mac ");
        }
    }

    public class MacTextBox : ITextBox
    {
        public void CreateTextBox()
        {
            Console.WriteLine($"Create a TextBox for Mac ");
        }
    }

    public interface IUIFactory
    {
        IButton CreateButton();
        ITextBox CreateTextBox();
    }

    public class WinUIFactory:IUIFactory
    {
        public IButton CreateButton()
        {
            return new WinButton();
        }

        public ITextBox CreateTextBox()
        {
            return new WinTextBox();
        }
    }

    public class MacUIFactory:IUIFactory
    {
        public IButton CreateButton()
        {
            return new MacButton();
        }

        public ITextBox CreateTextBox()
        {
            return new MacTextBox();
        }
    }

    public class ClientUI
    {
        private readonly IUIFactory _factory;

        public ClientUI(IUIFactory factory)
        {
            _factory = factory;
        }

        public void Render()
        {
            var button = _factory.CreateButton();
            var txtBox = _factory.CreateTextBox();
        }
    }



}
