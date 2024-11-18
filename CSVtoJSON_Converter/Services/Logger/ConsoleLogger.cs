using System;
using CSVtoJSON_Converter.Helpers;
using static CSVtoJSON_Converter.Handler.MainHandler;

namespace CSVtoJSON_Converter.Services.Logger
{
    public class ConsoleLogger : ILogger
    {
        public bool ShowLogInterface { get; set; }
        private const string SPACING = "   ";


        public ConsoleLogger()
        {
            InitLogSession();
        }


        private string GetDateAndTimeString()
        {
            DateTime now = DateTime.Now;
            return now.ToShortDateString() + " - " + now.ToLongTimeString() + "." + now.Millisecond;
        }

        private void CheckMode(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Success:
                    ChangeForground(Colors.SuccessForground);
                    break;
                case LogLevel.Info:
                    ChangeForground(Colors.DefaultForground);
                    break;

                case LogLevel.Warning:
                    ChangeForground(Colors.WarningForground);
                    break;

                case LogLevel.Error:
                    ChangeForground(Colors.ErrorForground);
                    break;

                case LogLevel.Fatal:
                    ChangeForground(Colors.FatalForground);
                    break;
            }
        }

        private string GetLogString(string text)
        {
            string loginfo = string.Empty;
            if (ShowLogInterface) loginfo = $"{ GetDateAndTimeString() }:{ SPACING }";
            return $">> {loginfo}{ text }";
        }


        public void InitLogSession()
        {
            if (ShowLogInterface)
                Console.WriteLine($">> { GetDateAndTimeString() }:{ SPACING }START OF LOGGING! --------------------");
        }

        public void EndLogSession()
        {
            if (ShowLogInterface)
                Console.WriteLine($">> { GetDateAndTimeString() }:{ SPACING }END OF LOGGING! ----------------------");
        }

        public void Log(LogLevel level, string text)
        {
            CheckMode(level);
            Console.Write(GetLogString(text));
            ChangeForground(Colors.DefaultForground);
        }

        public void LogL(LogLevel level, string text)
        {
            CheckMode(level);
            Console.WriteLine(GetLogString(text));
            ChangeForground(Colors.DefaultForground);
        }

        public void LogLine() => Console.WriteLine();


        public void Dispose()
        {
            EndLogSession();

            GC.SuppressFinalize(this);
        }
    }
}
