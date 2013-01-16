using System;
using Lounge.Core.Configuration;
using Moq;
using NUnit.Framework;

namespace Lounge.Tests.Configuration
{
    [TestFixture]
    public class CouchUriBuilderTests
    {
        private Mock<IProvideCouchConfiguration> mockCouchConfiguration;
        private readonly Uri testCouchServerUri = new Uri("http://someCouchServer/");
        private readonly Uri testCouchDatabaseUri = new Uri("http://someCouchServer/somedatabase/");

        [SetUp]
        public void BeforeEachTest()
        {
            mockCouchConfiguration = new Mock<IProvideCouchConfiguration>();
            mockCouchConfiguration.Setup(s => s.ServerUri).Returns(testCouchServerUri);
            mockCouchConfiguration.Setup(s => s.DatabaseUri).Returns(testCouchDatabaseUri);
            mockCouchConfiguration.Setup(s => s.DatabaseName).Returns("somedatabase");
        }

        [Test]
        public void ServerUriIsTheSameAsTheCouchConfiguration()
        {
            var couchUriBuilder = new CouchUriBuilder(mockCouchConfiguration.Object);

            Assert.That(couchUriBuilder.ServerUri, Is.EqualTo(mockCouchConfiguration.Object.ServerUri));
        }

        [Test]
        public void DatabaseUriIsTheSameAsTheCouchConfiguration()
        {
            var couchUriBuilder = new CouchUriBuilder(mockCouchConfiguration.Object);

            Assert.That(couchUriBuilder.DatabaseUri, Is.EqualTo(mockCouchConfiguration.Object.DatabaseUri));
        }

        [Test]
        public void DocumentUriIncludesDocumentId()
        {
            const string documentId = "1";
            var couchUriBuilder = new CouchUriBuilder(mockCouchConfiguration.Object);
            var expectedUrl = new Uri(testCouchDatabaseUri.AbsoluteUri + string.Format("/{0}", documentId)).AbsoluteUri;

            var documentUri = couchUriBuilder.DocumentUri(documentId);

            Assert.That(documentUri.AbsoluteUri, Is.EqualTo(expectedUrl));
        }

        [Test]
        public void ViewUriIncludesViewNameAndDesignDocumentName()
        {
            const string designDocumentName = "DesignName";
            const string viewName = "viewName";
            var couchUriBuilder = new CouchUriBuilder(mockCouchConfiguration.Object);
            var expectedUrl = string.Format("http://somecouchserver/somedatabase/_design/{0}/_view/{1}",
                                            designDocumentName, viewName);

            var viewUri = couchUriBuilder.ForView(designDocumentName, viewName);

            Assert.That(viewUri.AbsoluteUri, Is.EqualTo(expectedUrl));
        }

        [Test]
        public void ViewUriIncludesViewNameAndDesignDocumentNameWithKeyQueryString()
        {
            const string designDocumentName = "DesignName";
            const string viewName = "viewName";
            const string keyValue = "1";
            var couchUriBuilder = new CouchUriBuilder(mockCouchConfiguration.Object);
            var expectedUrl = string.Format("http://somecouchserver/somedatabase/_design/{0}/_view/{1}?key=%22{2}%22",
                                            designDocumentName, viewName, keyValue);
            
            var viewUri = couchUriBuilder.ForView(designDocumentName, viewName, keyValue);

            Assert.That(viewUri.AbsoluteUri, Is.EqualTo(expectedUrl));
        }

        [Test]
        public void ReturnsAllDatabasesUrl()
        {
            var couchUriBuilder = new CouchUriBuilder(mockCouchConfiguration.Object);
            const string expectedUrl = "http://somecouchserver/_all_dbs";;

            var documentUri = couchUriBuilder.ForAllDatabases();

            Assert.That(documentUri.AbsoluteUri, Is.EqualTo(expectedUrl));
        }
    }
}