using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace CSVtoJSON_Converter.Services.Serializer
{
    /// <summary>
    /// Serializes or deserializes objects to or from a JSON file.
    /// </summary>
    public class JSONSerializer : ISerializer
    {
        public void FileSave<T>(string filePath, T objectToWrite) where T : class, new()
        {
            using (StreamWriter sw = new StreamWriter(filePath, false))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, objectToWrite);
            }
        }

        public T FileRead<T>(string filePath) where T : class, new()
        {
            if (!File.Exists(filePath)) return new T();

            using (StreamReader sr = new StreamReader(filePath))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<T>(reader);
            }
        }

        public string SerializeObject<T>(T objectToSerialize)
        {
            StringBuilder sb = new StringBuilder();

            using (TextWriter tw = new StringWriter(sb))
            using (JsonWriter writer = new JsonTextWriter(tw))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, objectToSerialize);
            }

            return sb.ToString();
        }

        public T DeserializeObject<T>(string objectToDeserialize)
        {
            using (TextReader sr = new StringReader(objectToDeserialize))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<T>(reader);
            }
        }
    }
}
