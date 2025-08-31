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

    public abstract class Report
    {
        protected readonly IExportFormat _exportFormat;

        public Report(IExportFormat exportFormat)
        {
            _exportFormat = exportFormat;
        }

        public abstract void Generate();
    }

    public class SalesReport : Report
    {
        private readonly string _salesDate;
        public SalesReport(IExportFormat exportFormat, string salesDate) : base(exportFormat)
        {
            _salesDate = salesDate;
        }

        public override void Generate()
        {
            Console.WriteLine("جمع‌آوری داده‌های فروش...");
            string reportContent = $"گزارش فروش: {_salesDate}";
            _exportFormat.GenerateFile(reportContent,"SalesReport");
        }
    }

    public class InventoryReport : Report
    {
        private readonly string _inventoryData;
        public InventoryReport(IExportFormat exportFormat, string inventoryData) : base(exportFormat)
        {
            _inventoryData = inventoryData;
        }

        public override void Generate()
        {
            Console.WriteLine("جمع‌آوری داده‌های موجودی انبار...");
            string reportContent = $"گزارش موجودی: {_inventoryData}";
            _exportFormat.GenerateFile(reportContent, "InventoryReport");
        }
    }

    public class FinancialReport : Report
    {
        private readonly string _financialData;
        public FinancialReport(IExportFormat exportFormat, string financialData) : base(exportFormat)
        {
            _financialData = financialData;
        }

        public override void Generate()
        {
            Console.WriteLine("جمع‌آوری داده‌های مالی...");
            string reportContent = $"گزارش مالی: {_financialData}";
            _exportFormat.GenerateFile(reportContent, "FinancialReport");
        }
    }
}
