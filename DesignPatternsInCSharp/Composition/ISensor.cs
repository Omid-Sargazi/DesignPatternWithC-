using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsInCSharp.Composition
{
    public interface ISensor
    {
        string Read();
    }

    public class TemperatureSensor : ISensor
    {
        public string Read() => "Temp: 22°C";
    }

    public class HumiditySensor : ISensor
    {
        public string Read() => "Humidity: 40%";
    }

    public class Device
    {
        private readonly List<ISensor> _sensors = new();

        public void AddSensor(ISensor sensor) => _sensors.Add(sensor);

        public void ReadAll()
        {
            foreach (var sensor in _sensors)
                Console.WriteLine(sensor.Read());
        }
    }

}
