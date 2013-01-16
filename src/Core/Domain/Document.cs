using System;
using System.Linq;
using Lounge.Core.Configuration;
using Newtonsoft.Json;

namespace Lounge.Core.Domain
{
    [JsonObject]
    public class Document
    {
        [JsonProperty(CouchConfiguration.IdPropertyName)]
        public string Id { get; protected set; }
        [JsonProperty(CouchConfiguration.RevisionPropertyName)]
        public string Revision { get; protected internal set; }
        [JsonProperty("type")]
        public string DocumentType { get { return GetType().ToString().Split(new[] { '.' }).Last(); } }

        protected Document()
        {
            Id = Guid.NewGuid().ToString();
        }

        public override bool Equals(object obj)
        {
            var document = obj as Document;
            if (document == null)
                return false;       
            return document.Id == Id && document.Revision == Revision;
        }
        public override int GetHashCode()
        {
            return (Id + Revision).GetHashCode();
        }
    }
}
