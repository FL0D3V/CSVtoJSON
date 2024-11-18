using CSVtoJSON_Converter.Services.Logger;
using CSVtoJSON_Converter.Services.Serializer;

namespace CSVtoJSON_Converter.Systems
{
    public static class DI
    {
        private static ILogger mLogger = null;
        public static ILogger GetLogger()
            => mLogger ??= new ConsoleLogger();


        private static ISerializer mSerializer = null;
        public static ISerializer GetSerializer<T>() where T : class, ISerializer, new()
        {
            if (mSerializer == null) mSerializer = SerializerExtension.Create<T>();
            else if (mSerializer.GetType() != typeof(T)) mSerializer = mSerializer.Cast<T>();
            return mSerializer;
        }
    }
}
