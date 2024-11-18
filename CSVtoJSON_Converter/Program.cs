using System;
using System.Collections.Generic;
using CSVtoJSON_Converter.Helpers;
using CSVtoJSON_Converter.Services.Logger;
using CSVtoJSON_Converter.Systems;

namespace CSVtoJSON_Converter
{
    class Program
    {
        public static void Main(string[] args)
        {
            var logger = DI.GetLogger();
            logger.ShowLogInterface = false;

            try
            {
                var data = new List<string>();

                switch (args.Length)
                {
                    case 0:
                        logger.LogL(LogLevel.Info, Messages.NoArgs);

                        logger.Log(LogLevel.Info, Messages.EnterFilePath);
                        string filename = Console.ReadLine();

                        logger.Log(LogLevel.Info, Messages.EnterHeadingKey);
                        string heading = Console.ReadLine();
                        if (string.IsNullOrEmpty(heading)) heading = string.Empty;

                        logger.Log(LogLevel.Info, Messages.EnterExtraItems);
                        string items = Console.ReadLine();

                        if (string.IsNullOrEmpty(items)) data = null;
                        else
                        {
                            var splitted = items.Split(' ');
                            foreach (var s in splitted)
                                data.Add(s);
                        }

                        Converter.SetDataAndConvert(filename, heading, data);
                        break;

                    default:
                        logger.LogL(LogLevel.Info, Messages.Args);

                        switch (args.Length)
                        {
                            case 1:
                                Converter.SetDataAndConvert(args[0]);
                                break;

                            case 2:
                                Converter.SetDataAndConvert(args[0], args[1]);
                                break;

                            default:
                                for (int i = 2; i < args.Length; i++) data.Add(args[i]);
                                Converter.SetDataAndConvert(args[0], args[1], data);
                                break;
                        }
                        break;
                }
            }
            catch
            {
                logger.LogL(LogLevel.Error, Messages.WrongUsage);
            }

            logger.Log(LogLevel.Info, Messages.Closing);
            Console.Read();
            Console.ResetColor();
        }
    }
}
