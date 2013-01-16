using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Lounge.Core.Configuration;
using Lounge.Core.Domain;
using Lounge.Core.Json;

namespace Lounge.Core
{
    public class CouchClient: ITalkToCouch
    {
        private readonly Dictionary<Type, object> documentRepositoryFactory = new Dictionary<Type, object>();
        private readonly Dictionary<Type, object> viewRepositoryFactory = new Dictionary<Type, object>();

        private readonly IProvideCouchConfiguration couchConfiguration;
        private readonly CouchDatabase couchDatabase;
        private readonly CouchUriBuilder couchUriBuilder;
        private readonly JsonClient jsonClient;

        public CouchClient() : this(new CouchConfiguration(ConfigurationManager.AppSettings), new JsonClient())
        {
            couchDatabase = new CouchDatabase(GetType().Assembly);
            InitialiseDatabase();
        }

        public CouchClient(IProvideCouchConfiguration couchConfiguration, JsonClient jsonClient)
        {
            this.couchConfiguration = couchConfiguration;
            this.jsonClient = jsonClient;
            couchUriBuilder = new CouchUriBuilder(couchConfiguration);
        }

        public IEnumerable<string> DatabaseNames()
        {
            var jsonString = jsonClient.GetJsonFrom(couchUriBuilder.ForAllDatabases());
            var databases = jsonString.Deserialize<IEnumerable<string>>();
            return databases;
        }

        public bool DatabaseExists(string databaseName)
        {
            return DatabaseNames().Any(dbs => dbs == databaseName);
        }

        public IReadAndWriteDocuments<T> DocumentRepositoryFor<T>() where T : Document
        {
            var repositoryType = typeof (T);
            if (!documentRepositoryFactory.Keys.Contains(repositoryType))
            {
                var documentRepository = new DocumentRepository<T>(couchUriBuilder, jsonClient);
                documentRepositoryFactory.Add(repositoryType, documentRepository);
            }
            return (IReadAndWriteDocuments<T>)documentRepositoryFactory[repositoryType];
        }

        public IQueryAndWriteViews<T> ViewRepositoryFor<T>() where T : Document
        {
            var viewQueryType = typeof(T);
            if (!viewRepositoryFactory.Keys.Contains(viewQueryType))
            {
                var viewRepository = new ViewRepository<T>(couchUriBuilder, jsonClient);
                viewRepositoryFactory.Add(viewQueryType, viewRepository);
            }
            return (IQueryAndWriteViews<T>) viewRepositoryFactory[viewQueryType];
        }

        private void InitialiseDatabase()
        {
            if (!DatabaseExists(couchConfiguration.DatabaseName))
                jsonClient.SendJsonTo(couchUriBuilder.DatabaseUri, string.Empty, "PUT");
            var designDocumentRepository = DocumentRepositoryFor<DesignDocument>();
            couchDatabase.Initialise(designDocumentRepository);
        }
    }
}
