using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Lounge.Core.Json
{
    public static class SerializerExtensions
    {
        public static T Deserialize<T>(this string json) where T : class
        {
            var obj = JsonConvert.DeserializeObject<T>(json,  new[] {new KeyValuePairConverter()} );
            return obj;
        }

        public static string Serialize<T>(this T data) where T : class
        {
            string json = string.Empty;
            if (data == null) return json;
            json = JsonConvert.SerializeObject(data, new KeyValuePairConverter());
            return json;
        }
    }
}