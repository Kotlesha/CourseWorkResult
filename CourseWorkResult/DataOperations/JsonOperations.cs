using Newtonsoft.Json;

namespace CourseWorkResult.Json
{
    static class JsonOperations
    {
        public static string Serilalize<T>(T item) => JsonConvert.SerializeObject(item);

        public static T Deserealize<T>(string jsonString) => JsonConvert.DeserializeObject<T>(jsonString);
    }
}
