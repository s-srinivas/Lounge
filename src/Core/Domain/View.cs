using Newtonsoft.Json;

namespace Lounge.Core.Domain
{
    public class View
    {
        public View(string mapFunction = null, string reduceFunction = null)
        {
            Map = mapFunction;
            Reduce = reduceFunction;
        }

        [JsonProperty("map", NullValueHandling=NullValueHandling.Ignore)]
        public string Map { get; set; }
        [JsonProperty("reduce", NullValueHandling = NullValueHandling.Ignore)]
        public string Reduce { get; set; }
    }
}