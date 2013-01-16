using System;

namespace Lounge.Core.Configuration
{
    public class CouchUriBuilder
    {
        private readonly IProvideCouchConfiguration couchConfiguration;
        
        public CouchUriBuilder(IProvideCouchConfiguration couchConfiguration)
        {
            this.couchConfiguration = couchConfiguration;
        }

        public Uri ServerUri
        {
            get { return couchConfiguration.ServerUri; }
        }

        public Uri DatabaseUri
        {
            get { return couchConfiguration.DatabaseUri; }
        }

        public virtual Uri DocumentUri(string documentId)
        {
            var documentUri = new Uri(couchConfiguration.DatabaseUri.AbsoluteUri + string.Format("/{0}", documentId));
            return documentUri;
        }

        public virtual Uri ForView(string designDocumentName, string viewName)
        {
            return new Uri(couchConfiguration.DatabaseUri, string.Format("_design/{0}/_view/{1}", designDocumentName, viewName));
        }

        public virtual Uri ForView(string designDocumentName, string viewName, string keyValue)
        {   
            return new Uri(couchConfiguration.DatabaseUri, string.Format("_design/{0}/_view/{1}?key=\"{2}\"", designDocumentName, viewName, keyValue));
        }

        public virtual Uri ForGroupedView(string designDocumentName, string viewName)
        {
            return new Uri(couchConfiguration.DatabaseUri, string.Format("_design/{0}/_view/{1}?group=true", designDocumentName, viewName));
        }

        public Uri ForAllDatabases()
        {
            return new Uri(couchConfiguration.ServerUri, "_all_dbs");
        }
    }
}
