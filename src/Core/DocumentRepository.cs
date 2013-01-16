using System.Net;
using Lounge.Core.Configuration;
using Lounge.Core.Domain;
using Lounge.Core.Json;

namespace Lounge.Core
{
    public class DocumentRepository<T> : IReadAndWriteDocuments<T> where T : Document
    {
        private readonly CouchUriBuilder couchUriBuilder;
        private readonly JsonClient jsonClient;
        
        public DocumentRepository(CouchUriBuilder couchUriBuilder, JsonClient jsonClient)
        {
            this.couchUriBuilder = couchUriBuilder;
            this.jsonClient = jsonClient;
        }

        public string Create(T document)
        {
            try
            {
                var documentUri = couchUriBuilder.DocumentUri(document.Id);
                var json = document.Serialize();
                var jsonToSend = json.RemoveRevisionProperty();
                return jsonClient.SendJsonTo(documentUri, jsonToSend, "PUT");
            }
            catch (WebException exception)
            {
                var response = (HttpWebResponse) exception.Response;
                if (response.StatusCode == HttpStatusCode.Conflict)
                    throw new DuplicateDocumentException(exception.Message, exception);
                throw;
            }
        }
      
        public string Update(T document)
        {
            var documentUri = couchUriBuilder.DocumentUri(document.Id);
            var jsonToSend = document.Serialize();
            return jsonClient.SendJsonTo(documentUri, jsonToSend, "PUT");
        }

        public T Get(string documentId)
        {
            var documentUri = couchUriBuilder.DocumentUri(documentId);
            var json = jsonClient.GetJsonFrom(documentUri);
            return json.Deserialize<T>();
        }

        public bool HasDocument(T document)
        {
            bool hasDocument = false;
            try
            {
                var documentUri = couchUriBuilder.DocumentUri(document.Id);
                jsonClient.GetJsonFrom(documentUri);
                hasDocument = true;
            }   
            catch (WebException exception)
            {
                var response = (HttpWebResponse) exception.Response;
                if (response.StatusCode != HttpStatusCode.NotFound)
                    throw;
            }
            return hasDocument;
        }
    }
}