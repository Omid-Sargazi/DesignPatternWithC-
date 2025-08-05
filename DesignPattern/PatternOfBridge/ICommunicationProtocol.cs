using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.PatternOfBridge
{
    public interface ICommunicationProtocol
    {
        void Connect();
        void Disconnect();
        void SendCommand(string command);

    }

    public class WifiProtocol : ICommunicationProtocol
    {
        public void Connect()
        {
            Console.WriteLine($"connect to wifi");
        }

        public void Disconnect()
        {
           Console.WriteLine($"Disconnect to wifi");
        }

        public void SendCommand(string command)
        {
            Console.WriteLine($"Send commad:{command}");
        }
    }

    public class BlutoothProtocol : ICommunicationProtocol
    {
        public void Connect()
        {
            Console.WriteLine($"connect to Blutooth");
        }

        public void Disconnect()
        {
            Console.WriteLine($"Disconnect to Blutooth");

        }

        public void SendCommand(string command)
        {
            Console.WriteLine($"Send commad:{command}");
        }
    }

    public class RemoteProtocol : ICommunicationProtocol
    {
        public void Connect()
        {
            Console.WriteLine($"connect to Blutooth");
        }

        public void Disconnect()
        {
            Console.WriteLine($"Disconnect to Blutooth");
        }

        public void SendCommand(string command)
        {
            Console.WriteLine($"Send commad:{command}");
        }
    }

    public abstract class SmartDevice
    {
        protected readonly ICommunicationProtocol _protocol;
        protected SmartDevice(ICommunicationProtocol protocol)
        {
            _protocol = protocol;
        }

        public abstract void TurnOn();
        public abstract void TurnOff();
        public abstract void SetParameters(string parameters, int value);
    }

    public class SmartLigt : SmartDevice
    {
        private bool _isOn;
        private int _brightness;
        public SmartLigt(ICommunicationProtocol protocol) : base(protocol)
        {
            _isOn = false;
            _brightness = 50;
        }

        public override void SetParameters(string parameters, int value)
        {
            if (parameters.ToLower() == "brightness" && value >= 0 && value <= 100)
            {
                _protocol.Connect();
                _brightness = value;
                _protocol.SendCommand($"تنظیم روشنایی لامپ به {_brightness}%");
                Console.WriteLine($"روشنایی لامپ به {_brightness}% تنظیم شد");
                _protocol.Disconnect();
            }
        }

        public override void TurnOff()
        {
            _protocol.Connect();
            _protocol.SendCommand("turn off lump");
            _isOn=false;
            _protocol.Disconnect(); 
        }

        public override void TurnOn()
        {
           _protocol.Connect();
            _protocol.SendCommand($"روشن کردن لامپ با روشنایی {_brightness}%");
            _isOn = true;
            Console.WriteLine("لامپ روشن شد");
            _protocol.Disconnect();
        }
    }

    public class SmartThermostat : SmartDevice
    {
        private bool _isOn;
        private int _temperature;
        public SmartThermostat(ICommunicationProtocol protocol) : base(protocol)
        {
            _isOn = false;
            _temperature = 22;
        }

        public override void SetParameters(string parameters, int value)
        {
            if (parameters.ToLower() == "temperature" && value >= 15 && value <= 30)
            {
                _protocol.Connect();
                _temperature = value;
                _protocol.SendCommand($"تنظیم دمای ترموستات به {_temperature} درجه");
                Console.WriteLine($"دمای ترموستات به {_temperature} درجه تنظیم شد");
                _protocol.Disconnect();
            }
        }

        public override void TurnOff()
        {
            _protocol.Connect();
            _protocol.SendCommand("خاموش کردن ترموستات");
            _isOn = false;
            Console.WriteLine("ترموستات خاموش شد");
            _protocol.Disconnect();
        }

        public override void TurnOn()
        {
            _protocol.Connect();
            _protocol.SendCommand($"روشن کردن ترموستات با دمای {_temperature} درجه");
            _isOn = true;
            Console.WriteLine("ترموستات روشن شد");
            _protocol.Disconnect();
        }
    }
}
