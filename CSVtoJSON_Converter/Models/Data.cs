using System.Collections.Generic;

namespace CSVtoJSON_Converter.Models
{
    public class Data
    {
        public Dictionary<string, string> Heading { get; set; }
        public List<Dictionary<string, string>> CSV { get; set; }

        public Data(List<Dictionary<string, string>> csv, Dictionary<string, string> heading)
        {
            CSV = csv;
            Heading = heading;
        }
    }
}
