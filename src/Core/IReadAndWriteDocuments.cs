using Lounge.Core.Domain;

namespace Lounge.Core
{
    public interface IReadAndWriteDocuments<T> where T: Document
    {
        string Create(T document);
        string Update(T document);
        T Get(string documentId);
        bool HasDocument(T document);
    }
}