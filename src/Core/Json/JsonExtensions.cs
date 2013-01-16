using Lounge.Core.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lounge.Core.Json
{
    public static class JsonExtensions
    {
        public static string RemoveRevisionProperty(this string json)
        {
            var jsonDocumentObject = JObject.Parse(json);
            jsonDocumentObject.Remove(CouchConfiguration.RevisionPropertyName);
            var jsonToSend = jsonDocumentObject.ToString(Formatting.None);
            return jsonToSend;
        }
    }
}