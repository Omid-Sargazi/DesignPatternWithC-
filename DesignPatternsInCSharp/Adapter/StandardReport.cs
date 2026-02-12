using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DesignPatternsInCSharp.Adapter
{
    public class StandardReport
    {
        public string Title { get; set; }
        public DateTime GeneratedDate { get; set; }
        public List<ReportData> Data { get; set; }
    }

    public class ReportData
    {
        public string Key { get; set; }
        public decimal Value { get; set; }
    }

    // رابط خواندن گزارش
    public interface IReportReader
    {
        StandardReport ReadReport(string source);
    }

    // خواننده XML قدیمی
    public class LegacyXmlReader
    {
        public XDocument LoadXmlReport(string path)
        {
            string xml = @"
        <Report>
            <Title>گزارش فروش</Title>
            <Date>2026-02-12</Date>
            <Items>
                <Item name='محصول A' value='1500000'/>
                <Item name='محصول B' value='2300000'/>
            </Items>
        </Report>";

            return XDocument.Parse(xml);
        }
    }

    // خواننده JSON
    public class JsonReportReader
    {
        public string ReadJsonReport(string filePath)
        {
            return @"{
            ""reportTitle"": ""گزارش موجودی"",
            ""createDate"": ""2026-02-12T10:30:00"",
            ""items"": [
                {""itemName"": ""کالا 1"", ""amount"": 450000},
                {""itemName"": ""کالا 2"", ""amount"": 890000}
            ]
        }";
        }
    }

    // Adapter برای XML
    public class XmlReportAdapter : IReportReader
    {
        private readonly LegacyXmlReader _xmlReader;

        public XmlReportAdapter(LegacyXmlReader xmlReader)
        {
            _xmlReader = xmlReader;
        }

        public StandardReport ReadReport(string source)
        {
            XDocument doc = _xmlReader.LoadXmlReport(source);

            return new StandardReport
            {
                Title = doc.Root.Element("Title")?.Value,
                GeneratedDate = DateTime.Parse(doc.Root.Element("Date")?.Value),
                Data = doc.Root.Element("Items").Elements("Item")
                    .Select(item => new ReportData
                    {
                        Key = item.Attribute("name")?.Value,
                        Value = decimal.Parse(item.Attribute("value")?.Value)
                    }).ToList()
            };
        }
    }

    // Adapter برای JSON
    public class JsonReportAdapter : IReportReader
    {
        private readonly JsonReportReader _jsonReader;

        public JsonReportAdapter(JsonReportReader jsonReader)
        {
            _jsonReader = jsonReader;
        }

        public StandardReport ReadReport(string source)
        {
            string json = _jsonReader.ReadJsonReport(source);
            using JsonDocument document = JsonDocument.Parse(json);
            JsonElement root = document.RootElement;

            return new StandardReport
            {
                Title = root.GetProperty("reportTitle").GetString(),
                GeneratedDate = root.GetProperty("createDate").GetDateTime(),
                Data = root.GetProperty("items").EnumerateArray()
                    .Select(item => new ReportData
                    {
                        Key = item.GetProperty("itemName").GetString(),
                        Value = item.GetProperty("amount").GetDecimal()
                    }).ToList()
            };
        }
    }

    // سیستم پردازش گزارش
    public class ReportProcessor
    {
        private readonly IReportReader _reader;

        public ReportProcessor(IReportReader reader)
        {
            _reader = reader;
        }

        public void ProcessReport(string source)
        {
            StandardReport report = _reader.ReadReport(source);

            Console.WriteLine($"\n📊 {report.Title}");
            Console.WriteLine($"تاریخ: {report.GeneratedDate:yyyy/MM/dd}");
            Console.WriteLine("\nداده‌ها:");

            foreach (var item in report.Data)
            {
                Console.WriteLine($"  • {item.Key}: {item.Value:N0} تومان");
            }

            decimal total = report.Data.Sum(d => d.Value);
            Console.WriteLine($"\n💰 جمع کل: {total:N0} تومان");
        }
    }
}
