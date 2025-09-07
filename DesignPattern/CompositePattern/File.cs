using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.CompositePattern
{
    public interface IFileSystemComponent
    {
        string Name { get; }
        long GetSize();
        void Display(string indent = "");
    }
    public class File : IFileSystemComponent
    {
        public string Name { get; }
        private long  Size {get;}

        public File(string name, long size)
        {
            Name = name;
            Size = size;
        }
        public long GetSize()
        {
            return Size;
        }

        public void Display(string indent = "")
        {
            Console.WriteLine($"{indent}📄 {Name} ({Size} bytes)");
        }
    }


    public class Directory : IFileSystemComponent
    {

        private readonly List<IFileSystemComponent> _children = new List<IFileSystemComponent>();

        public void AddComponent(IFileSystemComponent component)
        {
            _children.Add(component);
        }

        public void RemoveComponent(IFileSystemComponent component)
        {
            _children.Remove(component);
        }
        public string Name { get; }
        public long GetSize()
        {
            long totalSize = 0;
            foreach (var component in _children)
            {
                totalSize += component.GetSize();
            }
            return totalSize;
        }

        public void Display(string indent = "")
        {
            Console.WriteLine($"{indent}📁 {Name}/");

            foreach (var component in _children)
            {
                component.Display(indent + "  ");
            }
        }
    }
}
