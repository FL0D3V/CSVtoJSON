using System.IO;
using System.Collections.Generic;
using CSVtoJSON_Converter.Models;
using CSVtoJSON_Converter.Handler;
using CSVtoJSON_Converter.Helpers;
using CSVtoJSON_Converter.Services.Logger;
using CSVtoJSON_Converter.Services.Serializer;
using System.Text;
using System.Linq;

namespace CSVtoJSON_Converter.Systems
{
    public static class Converter
    {
        public static void SetDataAndConvert(string filename, string key = "", List<string> itemList = null)
        {
            var logger = DI.GetLogger();

            string filedata;
            try   { filedata = File.ReadAllText(filename, Encoding.UTF8); }
            catch { logger.LogL(LogLevel.Error, Messages.FileNotFound); return; }

            logger.LogL(LogLevel.Success, Messages.Loaded);


            ConvertToJsonFile(filename, filedata, key, itemList);


            logger.LogL(LogLevel.Success, Messages.SuccessfullyConverted);
        }

        private static Data SplitData(string filedata, string key = "", List<string> itemList = null)
        {
            var list = new List<Dictionary<string, string>>();
            var heading = new Dictionary<string, string>();

            var csv = new List<List<string>>();

            var splittedLines = filedata.Split("\r\n");
            foreach (string line in splittedLines)
                csv.Add(line.Split(';').ToList());

            var properties = splittedLines[0].Split(';').ToList();

            properties.RemoveAll((x) => string.IsNullOrEmpty(x));
            if (itemList != null) foreach (var item in itemList) properties.Add(item);

            int maxElements = properties.Count;

            for (int line = 1; line < csv.Count; line++)
            {
                if (string.IsNullOrEmpty(splittedLines[line]))
                    break;

                var intern = new Dictionary<string, string>();

                intern.Add("Id", (line - 1).ToString());

                for (int col = 0; col < maxElements; col++)
                {
                    if (key != properties[col])
                    {
                        string data = string.Empty;
                        string title = properties[col];

                        var splitted = title.Split(':');

                        if (splitted.Length == 1)
                            data = csv[line][col];
                        else
                        {
                            title = splitted[0];
                            data = splitted[1];
                        }

                        intern.Add(title, data);
                    }
                    else
                    {
                        if (heading.Count <= 0)
                            heading.Add(properties[col], csv[line][col]);
                    }
                }

                list.Add(intern);
            }

            return new Data(list, heading);
        }

        private static void ConvertToJsonFile(string filepath, string filedata, string key = "", List<string> itemList = null)
        {
            var logger = DI.GetLogger();
            logger.LogL(LogLevel.Info, Messages.NowConverting);


            Data data = SplitData(filedata, key, itemList);


            string path = Path.Combine(Path.GetDirectoryName(filepath), Path.GetFileNameWithoutExtension(filepath));

            if (path.Contains(Path.DirectorySeparatorChar)) path = path.Substring(0, path.LastIndexOf(Path.DirectorySeparatorChar));
            if (path == Path.GetFileNameWithoutExtension(filepath)) path = string.Empty;

            string pathData = Path.Combine(path, MainHandler.GetDataFilePath());
            string pathHeading = Path.Combine(path, MainHandler.GetHeadingFilePath());

            Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(filepath), MainHandler.GetDataFolder()));


            var serializer = DI.GetSerializer<JSONSerializer>();
            
            using (var writer = new StreamWriter(pathData, false, Encoding.UTF8))
                writer.Write(serializer.SerializeObject(data.CSV));

            if (!string.IsNullOrEmpty(key) && data.Heading.Count == 0)
            {
                logger.LogL(LogLevel.Warning, Messages.NoEntryFound);
                return;
            }

            if (data.Heading.Count != 0)
            {
                using (var writer = new StreamWriter(pathHeading, false, Encoding.UTF8))
                    writer.Write(serializer.SerializeObject(data.Heading));
            }
        }
    }
}
