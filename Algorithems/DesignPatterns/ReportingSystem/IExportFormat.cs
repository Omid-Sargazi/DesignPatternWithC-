using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithems.DesignPatterns.ReportingSystem
{
    public interface IExportFormat
    {
        void GenerateFile(string data, string fileName);
    }

    public class PdfExport : IExportFormat
    {
        public void GenerateFile(string data, string fileName)
        {
            Console.WriteLine($"در حال تولید فایل PDF: {fileName}.pdf");
            Console.WriteLine($"محتوای PDF: {data}");
        }
    }

    public class ExcelExport : IExportFormat
    {
        public void GenerateFile(string data, string fileName)
        {
            Console.WriteLine($"در حال تولید فایل Excel: {fileName}.xlsx");
            Console.WriteLine($"داده‌های Excel: {data}");
        }
    }

    public class HtmlExport : IExportFormat
    {
        public void GenerateFile(string data, string fileName)
        {
            Console.WriteLine($"در حال تولید فایل HTML: {fileName}.html");
            Console.WriteLine($"کد HTML: <html><body>{data}</body></html>");
        }
    }
}
