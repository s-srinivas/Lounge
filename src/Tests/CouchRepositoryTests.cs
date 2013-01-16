using System;
using NUnit.Framework;

namespace Lounge.Tests
{
    public class CouchRepositoryTests
    {
        private readonly Uri couchUri = new Uri("http://couch:1433/");
        

        [SetUp]
        public void BeforeEachTest()
        {
            
        }

        //[Test]
        //public void ShouldCallGetOnTheRestClient()
        //{
        //    var expectedUri = new Uri(couchUri.AbsoluteUri + "/1");
        //    var testData = new TestDocument("1");
        //    mockRestClient.Setup(c => c.Get(expectedUri)).Returns(testData.Serialize());
        //    var repository = new CouchRepository<TestDocument>(couchUri, mockRestClient.Object);

        //    repository.Get("1");

        //    mockRestClient.Verify(c => c.Get(expectedUri));
        //}

        //[Test]
        //public void ShouldCallPostOnTheRestClientForSaveOrUpdate()
        //{
        //    var expectedUri = new Uri(couchUri.AbsoluteUri + "/1");
        //    var testData = new TestDocument("1");
        //    mockRestClient.Setup(c => c.Post(testData, expectedUri)).Returns(testData.Serialize());
        //    var repository = new CouchRepository<TestDocument>(couchUri, mockRestClient.Object);

        //    repository.Update(testData);

        //    mockRestClient.Verify(c => c.Post(testData, expectedUri));
        //}

        //[Test]
        //public void ShouldCallPutOnTheRestClientForCreate()
        //{
        //    var expectedUri = new Uri(couchUri.AbsoluteUri + "/1");
        //    var testData = new TestDocument("1");
        //    mockRestClient.Setup(c => c.Put(testData, expectedUri)).Returns(testData.Serialize());
        //    var repository = new CouchRepository<TestDocument>(couchUri, mockRestClient.Object);

        //    repository.Create(testData);

        //    mockRestClient.Verify(c => c.Put(testData, expectedUri));
        //}
    }
}
