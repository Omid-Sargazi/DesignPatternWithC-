using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsWithC_.StructuralPatterns.Composition
{
    public abstract class ContentComponent
    {
        public string Name { get; set; }
        protected ContentComponent(string name)
        {
            Name = name;
        }

        public abstract void Display(int depth);
        public abstract int GetSize();
    }

    public class File : ContentComponent
    {
        private int Size;
        public File(string name, int size) : base(name)
        {
            Size = size;
        }

        public override void Display(int depth)
        {
           Console.WriteLine(new string('-',depth)+$"{Name} Size: {Size}");
        }

        public override int GetSize()
        {
            return Size;
        }
    }

    public class Folder : ContentComponent
    {
        private List<ContentComponent> _children = new List<ContentComponent>();
        public Folder(string name) : base(name)
        {
        }

        public void Add(ContentComponent component)
        {
            _children.Add(component);
        }

        public void Remove(ContentComponent component)
        {
            _children.Remove(component);
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + $"{Name}");
            foreach(var component in _children)
            {
                component.Display(depth+2);
            }
        }

        public override int GetSize()
        {
            int totalSize = 0;
            foreach(var component in _children)
            {
                totalSize += component.GetSize();
            }
            return totalSize;
        }
    }
}
