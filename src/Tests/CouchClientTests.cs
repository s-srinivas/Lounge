using System;
using System.Linq;
using Lounge.Core;
using Lounge.Core.Configuration;
using Lounge.Core.Json;
using Lounge.Tests.TestData;
using Moq;
using NUnit.Framework;

namespace Lounge.Tests
{
    [TestFixture]
    public class CouchClientTests
    {
        private readonly Mock<IProvideCouchConfiguration> mockConfiguration = new Mock<IProvideCouchConfiguration>();
        private readonly Mock<JsonClient> mockJsonClient = new Mock<JsonClient>();
        private CouchClient couchClient;
        private readonly Uri testCouchServerUri = new Uri("http://somecouchserver/");

        [SetUp]
        public void BeforeEachTest()
        {
            couchClient = new CouchClient(mockConfiguration.Object, mockJsonClient.Object);
            mockConfiguration.Setup(c => c.ServerUri).Returns(testCouchServerUri);
        }

        [Test]
        public void CallsGetJsonWithAllDatabasesUriToGetDatabaseNames()
        {
            var calledUri = new Uri("http://localhost");
            mockJsonClient.Setup(c => c.GetJsonFrom(It.IsAny<Uri>())).Callback<Uri>(r => calledUri = r).Returns("[\"database\"]");

            var databaseNames = couchClient.DatabaseNames();

            Assert.That(calledUri.AbsoluteUri, Is.EqualTo("http://somecouchserver/_all_dbs"));
            Assert.That(databaseNames.First(), Is.EqualTo("database"));
        }

        [Test]
        public void ReturnsTrueWhenDatabaseIsPresentOnServer()
        {
            const string allDatabasesJson = "[\"database1\", \"database2\"]";
            mockJsonClient.Setup(c => c.GetJsonFrom(It.IsAny<Uri>())).Returns(allDatabasesJson);

            var databaseExists = couchClient.DatabaseExists("database1");

            Assert.That(databaseExists, Is.EqualTo(true));
        }

        [Test]
        public void ReturnsFalseWhenDatabaseIsNotPresentOnServer()
        {
            const string allDatabasesJson = "[\"database1\", \"database2\"]";
            mockJsonClient.Setup(c => c.GetJsonFrom(It.IsAny<Uri>())).Returns(allDatabasesJson);

            var databaseExists = couchClient.DatabaseExists("database3");

            Assert.That(databaseExists, Is.EqualTo(false));
        }


        [Test]
        public void ReturnsTheSameDocumentRepositoryWhenCalledTwice()
        {
            var repository1 = couchClient.DocumentRepositoryFor<TestDocument>();
            var repository2 = couchClient.DocumentRepositoryFor<TestDocument>();

            Assert.That(repository1, Is.EqualTo(repository2));
        }

        [Test]
        public void ReturnsTheSameViewRepositoryWhenCalledTwice()
        {
            var viewRepository1 = couchClient.ViewRepositoryFor<TestDocument>();
            var viewRepository2 = couchClient.ViewRepositoryFor<TestDocument>();

            Assert.That(viewRepository1, Is.EqualTo(viewRepository2));
        }

    }
}
