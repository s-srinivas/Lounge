using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lounge.Core.Domain
{
    public abstract class DesignDocument : Document
    {
        private readonly string designDocumentName;

        protected DesignDocument(string designDocumentName)
        {
            this.designDocumentName = designDocumentName;
            Language = "javascript";
            Views = new Dictionary<string, View>();
            InitialiseId();
        }

        protected void InitialiseId()
        {
            Id = string.Format("_design/{0}", designDocumentName);
        }

        [JsonProperty("language")]
        public string Language { get; private set; }

        [JsonProperty("views")]
        public Dictionary<string, View> Views { get; internal set; }

        public void AddView(string name, View view)
        {
            Views.Add(name, view);
        }
    }
}
