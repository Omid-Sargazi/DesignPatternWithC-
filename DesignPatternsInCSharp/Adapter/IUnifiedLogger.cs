using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsInCSharp.Adapter
{
    public interface IUnifiedLogger
    {
        void LogInfo(string message);
        void LogError(string message, Exception ex);
        void LogWarning(string message);
    }

    // کتابخانه قدیمی Log4Net
    public class Log4NetLogger
    {
        public void Info(string msg)
        {
            Console.WriteLine($"[Log4Net INFO]: {msg}");
        }

        public void Error(string msg, Exception ex)
        {
            Console.WriteLine($"[Log4Net ERROR]: {msg} - {ex.Message}");
        }
    }

    // کتابخانه NLog
    public class NLogLogger
    {
        public void WriteInfo(string text)
        {
            Console.WriteLine($"[NLog] INFO: {text}");
        }

        public void WriteError(string text)
        {
            Console.WriteLine($"[NLog] ERROR: {text}");
        }
    }

    // Adapter برای Log4Net
    public class Log4NetAdapter : IUnifiedLogger
    {
        private readonly Log4NetLogger _logger;

        public Log4NetAdapter(Log4NetLogger logger)
        {
            _logger = logger;
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public void LogError(string message, Exception ex)
        {
            _logger.Error(message, ex);
        }

        public void LogWarning(string message)
        {
            _logger.Info($"WARNING: {message}");
        }
    }

    // Adapter برای NLog
    public class NLogAdapter : IUnifiedLogger
    {
        private readonly NLogLogger _logger;

        public NLogAdapter(NLogLogger logger)
        {
            _logger = logger;
        }

        public void LogInfo(string message)
        {
            _logger.WriteInfo(message);
        }

        public void LogError(string message, Exception ex)
        {
            _logger.WriteError($"{message} - Exception: {ex.Message}");
        }

        public void LogWarning(string message)
        {
            _logger.WriteInfo($"⚠️ {message}");
        }
    }
}
