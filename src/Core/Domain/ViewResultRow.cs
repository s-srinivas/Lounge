using Newtonsoft.Json;

namespace Lounge.Core.Domain
{
    public class ViewResultRow<T>
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("value")]
        public T Value { get; set; }
    }
}