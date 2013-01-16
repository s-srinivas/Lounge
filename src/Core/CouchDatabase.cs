using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lounge.Core.Domain;

namespace Lounge.Core
{
    public class CouchDatabase
    {
        private readonly List<DesignDocument>  designDocuments = new List<DesignDocument>();

        public CouchDatabase(Assembly assembly)
        {
            var designDocumentTypes = assembly.GetTypes().Where(p => p.IsSubclassOf(typeof(DesignDocument)));
            foreach (var designDocumentType in designDocumentTypes)
            {
                var designDocument = (DesignDocument)Activator.CreateInstance(designDocumentType);
                designDocuments.Add(designDocument);
            }
        }

        public void Initialise(IReadAndWriteDocuments<DesignDocument> designDocumentRepository)
        {
            foreach (var designDocument in designDocuments)
            {
                if (!designDocumentRepository.HasDocument(designDocument))
                {
                    designDocumentRepository.Create(designDocument);
                }
            }
        }
    }
}
