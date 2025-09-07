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
}
