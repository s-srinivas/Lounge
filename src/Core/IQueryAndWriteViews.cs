using System.Collections.Generic;

namespace Lounge.Core
{
    public interface IQueryAndWriteViews<out T>
    {
        IEnumerable<T> GetDocuments(string designDocumentName, string viewName);
        T GetDocument(string designDocumentName, string viewName, string key);
    }
}