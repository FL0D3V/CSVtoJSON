namespace CSVtoJSON_Converter.Services.Serializer
{
    /// <summary>
    /// The Base Interface for all Serializers.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serializes the given object to the given filepath.
        /// </summary>
        /// <typeparam name="T">The Type that will be serialized</typeparam>
        /// <param name="filePath">The path to serialize the object</param>
        /// <param name="objectToWrite">The object to serialize</param>
        void FileSave<T>(string filePath, T objectToWrite) where T : class, new();


        /// <summary>
        /// Read the file and deserialize it to the type given.
        /// </summary>
        /// <typeparam name="T">The type the deserialized data should get</typeparam>
        /// <param name="filePath">The file you want to deserialize</param>
        /// <returns>Returns a new Instance of the Type set. It will have the data read from the file inside.</returns>
        T FileRead<T>(string filePath) where T : class, new();



        /// <summary>
        /// Serializes a given object to a string.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="objectToSerialize">The object that gets serialized</param>
        /// <returns>the serialized object</returns>
        string SerializeObject<T>(T objectToSerialize); //where T : class, new();


        /// <summary>
        /// Deserializes the given string to a object.
        /// </summary>
        /// <typeparam name="T">The type of the object for deserialization</typeparam>
        /// <param name="objectToDeserialize">The object that gets deserialized</param>
        /// <returns>The new object</returns>
        T DeserializeObject<T>(string objectToDeserialize); //where T : class, new();
    }


    /// <summary>
    /// The extensions class for all loggers.
    /// </summary>
    public static class SerializerExtension
    {
        /// <summary>
        /// Casts a given serialzer to another serializer.
        /// </summary>
        /// <typeparam name="T">The type of the new serializer</typeparam>
        /// <param name="serializer">the old serialzer</param>
        /// <returns>The converted serializer</returns>
        public static ISerializer Cast<T>(this ISerializer serializer) where T : class, ISerializer, new() => serializer as T;

        /// <summary>
        /// Creates a new serializer with the given type.
        /// </summary>
        /// <typeparam name="T">The type of the serializer</typeparam>
        /// <returns>A new serializer object of the given type</returns>
        public static ISerializer Create<T>() where T : class, ISerializer, new() => new T();
    }
}
