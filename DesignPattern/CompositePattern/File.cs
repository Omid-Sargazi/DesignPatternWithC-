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


    public interface ITarget
    {
        string GetNewRequest();
    }

    public class Adaptee
    {
        public string GetOldRequest()
        {
            return $"Old Request;";
        }
    }

    public class Adapter : ITarget
    {
        private Adaptee _adaptee;
        public string GetNewRequest()
        {
            return _adaptee.GetOldRequest() + $"Add New Request with Adapter pattern.";
        }
    }


    public interface IReportFormat
    {
        void Generate(string context);
        string GetFileExtension();
    }

    public class PdfFormat : IReportFormat
    {
        public void Generate(string context)
        {
            Console.WriteLine($"generating PDF report: {context}");
        }

        public string GetFileExtension()
        {
            return ".pdf";
        }
    }

    public class WordFormat : IReportFormat
    {
        public void Generate(string context)
        {
           Console.WriteLine($"generating Word report: {context}");
        }

        public string GetFileExtension()
        {
            return ".word";
        }
    }

    public class ExcelFormat : IReportFormat
    {
        public void Generate(string context)
        {
            Console.WriteLine($"Generation Excel format: {context}");
        }

        public string GetFileExtension()
        {
            return ".excel";
        }
    }

    public abstract class Report
    {
        protected IReportFormat _format;

        protected Report(IReportFormat format)
        {
            _format = format;
        }


        public void SetFromat(IReportFormat format)
        {
            _format = format;
        }

        public abstract void Generate();
    }
}
