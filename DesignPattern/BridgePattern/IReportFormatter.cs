using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.BridgePattern
{
    public interface IReportFormatter
    {
        void GenerateHeader();
        void GenerateFooter();
        void GenerateBody();
        byte[] GetFormatReport();
    }

    public class PdfReportFormatter : IReportFormatter
    {
        public void GenerateBody()
        {
            throw new NotImplementedException();
        }

        public void GenerateFooter()
        {
            throw new NotImplementedException();
        }

        public void GenerateHeader()
        {
            throw new NotImplementedException();
        }

        public byte[] GetFormatReport()
        {
            throw new NotImplementedException();
        }
    }

    public class ExcelReportFormatter : IReportFormatter
    {
        public void GenerateBody()
        {
            throw new NotImplementedException();
        }

        public void GenerateFooter()
        {
            throw new NotImplementedException();
        }

        public void GenerateHeader()
        {
            throw new NotImplementedException();
        }

        public byte[] GetFormatReport()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class Report
    {
        private protected IReportFormatter _formatter;
        protected Report(IReportFormatter formatter)
        {
            _formatter = formatter;
        }

        public abstract void GenerateReport();
        public byte[] GetReport()=>_formatter.GetFormatReport();
    }

    public class ProductReport : Report
    {
        public ProductReport(IReportFormatter formatter) : base(formatter)
        {
        }

        public override void GenerateReport()
        {
            _formatter.GetFormatReport();
        }
    }

    public class UserReport : Report
    {
        public UserReport(IReportFormatter formatter) : base(formatter)
        {
        }

        public override void GenerateReport()
        {
            _formatter.GetFormatReport();
        }
    }
}
