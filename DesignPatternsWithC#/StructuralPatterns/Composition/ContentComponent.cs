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

    public class CreateComposite
    {
        public static void Run()
        {
            var file1 = new File("doc.txt", 100);
            var file2 = new File("doc.jpg", 4500);
            var file3 = new File("doc.csv", 4500);
            var file4 = new File("doc.doc", 200);
            var file5 = new File("doc.txt", 300);

            var folder1 = new Folder("Documents");
            folder1.Add(file1);
            folder1.Add(file2);
            folder1.Add(file3);

            var folder2 = new Folder("Images");
            folder2.Add(file2);

            var rootFolder = new Folder("Root");
            rootFolder.Add(folder1);
            rootFolder.Add(folder2);
            rootFolder.Add(file4);

            Console.WriteLine("File System Structure:");
            rootFolder.Display(1);

           
            Console.WriteLine($"\nTotal size: {rootFolder.GetSize()} KB");

        }
    }
}
