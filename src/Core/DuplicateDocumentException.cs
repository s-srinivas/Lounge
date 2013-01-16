using System;
using System.Net;

namespace Lounge.Core
{
    [Serializable]
    public class DuplicateDocumentException : Exception
    {
        public DuplicateDocumentException(){}

        public DuplicateDocumentException(string message, WebException exception) : base(message, exception)
        {
        }
    }
}