using System.Collections.Generic;
using Lounge.Core.Domain;

namespace Lounge.Core
{
    public interface ITalkToCouch 
    {
        IEnumerable<string> DatabaseNames();
        bool DatabaseExists(string databaseName);
        IReadAndWriteDocuments<T> DocumentRepositoryFor<T>() where T : Document;
        IQueryAndWriteViews<T> ViewRepositoryFor<T>() where T : Document;
    }
}