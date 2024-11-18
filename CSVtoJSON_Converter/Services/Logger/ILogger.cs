using System;

namespace CSVtoJSON_Converter.Services.Logger
{
    public interface ILogger : IDisposable
    {
        public bool ShowLogInterface { get; set; }

        public void Log(LogLevel level, string text);
        public void LogL(LogLevel level, string text);
        public void LogLine();

        public void InitLogSession();
        public void EndLogSession();
    }
}
