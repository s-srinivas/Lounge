using System.Collections.Generic;
using System.Linq;
using Lounge.Core.Configuration;
using Lounge.Core.Domain;
using Lounge.Core.Json;

namespace Lounge.Core
{
    internal class ViewRepository<T>: IQueryAndWriteViews<T>
    {
        private readonly CouchUriBuilder couchUriBuilder;
        private readonly JsonClient jsonClient;

        public ViewRepository(CouchUriBuilder couchUriBuilder, JsonClient jsonClient)
        {
            this.couchUriBuilder = couchUriBuilder;
            this.jsonClient = jsonClient;
        }

        public IEnumerable<T> GetDocuments(string designDocumentName, string viewName)
        {
            var viewUri = couchUriBuilder.ForView(designDocumentName, viewName);
            var json = jsonClient.GetJsonFrom(viewUri);
            var viewDataResult = json.Deserialize<ViewResult<T>>();
            return viewDataResult.Rows.Select(s => s.Value);
        }

        public T GetDocument(string designDocumentName, string viewName, string key)
        {
            var viewUri = couchUriBuilder.ForView(designDocumentName, viewName, key);
            var json = jsonClient.GetJsonFrom(viewUri);
            var viewDataResult = json.Deserialize<ViewResult<T>>();
            return viewDataResult.Rows.Select(s => s.Value).FirstOrDefault();
        }
    }
}