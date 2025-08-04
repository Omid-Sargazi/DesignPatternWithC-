using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DesignPattern.AdapterPattern
{
    public class XmlReportGenerator
    {
        public void GenerateReport(string xmlData)
        {
            Console.WriteLine("📄 Report generated with XML:\n" + xmlData);
        }
    }

    public class JsonDataProvider
    {
        public string GetJson()
        {
            return "{ \"name\": \"Omid\", \"score\": 95 }";
        }
    }

    public class JsonToXmlAdapter
    {
        private readonly JsonDataProvider _provider;
        public JsonToXmlAdapter(JsonDataProvider provider)
        {
            _provider = provider;
        }

        public string GetXmlData()
        {
            var doc = _provider.GetJson();
            return doc?.ToString();
        }
    }
}
