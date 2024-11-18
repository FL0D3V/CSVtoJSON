using System;
using System.IO;

namespace CSVtoJSON_Converter.Handler
{
    public static class MainHandler
    {
        private const string FileNameData = "data.json";
        private const string FileNameHeading = "heading.json";

        private const string FileFolder = "data";


        public static string GetDataFolder() => FileFolder;

        public static string GetDataFilePath() => Path.Combine(FileFolder, FileNameData);
        public static string GetHeadingFilePath() => Path.Combine(FileFolder, FileNameHeading);


        public static void ChangeForground(ConsoleColor color)
            => Console.ForegroundColor = color;
    }
}
