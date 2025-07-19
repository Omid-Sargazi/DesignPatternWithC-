using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopositionPattern.CompositionPattern
{
    public interface IMenuComponent
    {
        void Display(int indent =0);
    }

    public class MenuItem : IMenuComponent
    {
        private readonly string _title;
        public MenuItem(string title)
        {
            _title = title;
        }
        public void Display(int indent = 0)
        {
           Console.WriteLine(new string(' ',indent)+"_"+_title);
        }
    }

    public class Menu : IMenuComponent
    {
        private readonly string _title;
        private readonly List<IMenuComponent> _items = new();
        public Menu(string title)
        {
            _title=title;
        }

        public void Add(IMenuComponent item)
        {
            _items.Add(item);
        }
        public void Display(int indent = 0)
        {
            Console.WriteLine(new string(' ',indent) +_title);
            foreach(var item in _items)
            {
                item.Display(indent+2);
            }
        }
    }


    public class CompositeClient
    {
        public static void Display()
        {
            var burger = new MenuItem("burger");
            var fries = new MenuItem("Fries");
            var juice = new MenuItem("Huice");


            var kidsmenu = new Menu("Kids Menu");
            kidsmenu.Add(juice);

            var mainMenu = new Menu("Main Menu");
            mainMenu.Add(burger);
            mainMenu.Add(fries);
            mainMenu.Add(kidsmenu);

            mainMenu.Display();
            
        }
    }
}
