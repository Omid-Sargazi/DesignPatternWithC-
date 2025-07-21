using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsInCSharp.BridgePattern
{
    public interface IReportExporter
    {
        void Export(string reportType, string content);
    }

    public class PDFExporter : IReportExporter
    {
        public void Export(string reportType, string content)
        {
            throw new NotImplementedException();
        }
    }

    public class ExcelExporter : IReportExporter
    {
        public void Export(string reportType, string content)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class Report
    {
        protected IReportExporter _exporter;
        protected Report(IReportExporter exporter)
        { 
            _exporter = exporter; 
        }

        public abstract void Generate(string content);
    }

    public class SalesReport : Report
    {
        public SalesReport(IReportExporter exporter) : base(exporter)
        {
        }

        public override void Generate(string content)
        {
            throw new NotImplementedException();
        }
    }

    public class InventoryReport : Report
    {
        public InventoryReport(IReportExporter exporter) : base(exporter)
        {
        }

        public override void Generate(string content)
        {
            throw new NotImplementedException();
        }
    }
}
