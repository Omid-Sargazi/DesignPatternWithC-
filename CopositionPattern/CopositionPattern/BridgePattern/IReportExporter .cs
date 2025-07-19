using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopositionPattern.BridgePattern
{
    public interface IReportExporter
    {
        void Generate(string reportType, string context);
    }

    public class PDFExporter : IReportExporter
    {
        public void Generate(string typeExporter, string context)
        {
            Console.WriteLine($"{typeExporter}with context{context}");
        }
    }


    public class ExcelExporter : IReportExporter
    {
        public void Generate(string typeExporter, string context)
        {
            Console.WriteLine($"{typeExporter}with context{context}");

        }
    }

    public abstract class Report
    {
        protected IReportExporter _exporter;
        protected Report(IReportExporter exporter)
        {
            _exporter = exporter;
        }

        public abstract void Generate(string context);
    }

    public class SalesReport : Report
    {
        public SalesReport(IReportExporter exporter) : base(exporter)
        {
        }

        public override void Generate(string context)
        {
            _exporter.Generate($"[Sales]",context);
        }
    }

    public class InventoryReport : Report
    {
        public InventoryReport(IReportExporter exporter) : base(exporter)
        {
        }

        public override void Generate(string context)
        {
            _exporter.Generate($"[Inventory]",context);
        }
    }
}
