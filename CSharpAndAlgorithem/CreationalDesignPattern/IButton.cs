using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAndAlgorithem.CreationalDesignPattern
{
    public interface IButton
    {
        void CreateButton();
    }

    public interface ITextBox
    {
        void CreateTextBox();
    }

    public interface ICheckBox
    {
        void CreateCheckBox();
    }

    public interface IDialogBox
    {
        public void CreateDialogBox();
    }

    public class WinButton:IButton
    {
        public void CreateButton()
        {
           Console.WriteLine("Created a Button for Windows");
        }
    }

    public class WinTextBox:ITextBox
    {
        public void CreateTextBox()
        {
            Console.WriteLine("Created a TextBox for Windows");
        }
    }

    public class WinCheckBox:ICheckBox
    {
        public void CreateCheckBox()
        {
            Console.WriteLine("Created a CheckBox for Windows");
        }
    }

    public class WinDialogBox:IDialogBox
    {
        public void CreateDialogBox()
        {
            Console.WriteLine("Created a DialogBox for Windows");
        }
    }

    public class MacButton:IButton
    {
        public void CreateButton()
        {
            Console.WriteLine("Created a Button for Mac");
        }
    }

    public class MacTextBox:ITextBox
    {
        public void CreateTextBox()
        {
            Console.WriteLine("Created a TextBox for Mac");
        }
    }
    public class MacCheckBox:ICheckBox
    {
        public void CreateCheckBox()
        {
            Console.WriteLine("Created a CheckBox for Mac");
        }
    }

    public class DialogBox : IDialogBox
    {
        public void CreateDialogBox()
        {
            Console.WriteLine("Created a DialogBox for Mac");
        }
    }

    public interface IUIFactory
    {
        IButton CreateButton();
        ITextBox CreateTextBox();
        ICheckBox CreateCheckBox();
        IDialogBox CreateDialogBox();
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

        public ICheckBox CreateCheckBox()
        {
            return new WinCheckBox();
        }

        public IDialogBox CreateDialogBox()
        {
            return new WinDialogBox();
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

        public ICheckBox CreateCheckBox()
        {
            return new MacCheckBox();
        }

        public IDialogBox CreateDialogBox()
        {
            return new DialogBox();
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
            _factory.CreateButton();
            _factory.CreateTextBox();
            _factory.CreateCheckBox();
            _factory.CreateDialogBox();
        }
    }
}
