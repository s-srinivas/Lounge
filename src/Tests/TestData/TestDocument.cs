using System;
using Lounge.Core.Domain;

namespace Lounge.Tests.TestData
{
    [Serializable]
    public class TestDocument : Document
    {
        public TestDocument() { }
        public TestDocument(string id)
        {
            Id = id;
        }
    }
}