using System;
using Lounge.Core;
using Lounge.Core.Configuration;
using Lounge.Core.Json;
using Lounge.Tests.TestData;
using Moq;
using NUnit.Framework;

namespace Lounge.Tests
{
    [TestFixture]
    public class DocumentRepositoryTests
    {
        private readonly Mock<JsonClient> mockJsonClient = new Mock<JsonClient>();
        private Mock<IProvideCouchConfiguration> mockConfiguration;
        private Mock<CouchUriBuilder> mockCouchUriBuilder;
        private DocumentRepository<TestDocument> documentRepository;
        private Uri documentUri;

        [SetUp]
        public void BeforeEachTest()
        {
            mockConfiguration = new Mock<IProvideCouchConfiguration>();
            mockCouchUriBuilder = new Mock<CouchUriBuilder>(new object[] {mockConfiguration.Object});
            documentRepository = new DocumentRepository<TestDocument>(mockCouchUriBuilder.Object, mockJsonClient.Object);
            documentUri = new Uri("http://somecouchserver:1433/db/someid");
            mockCouchUriBuilder.Setup(s => s.DocumentUri(It.IsAny<string>())).Returns(documentUri);
        }

        [Test]
        public void CreateDocumentCallsSendJsonWithPutToDocumentUri()
        {
            const string expectedJson = "{\"ok\":\"true\"}";
            var testDocument = new TestDocument("someid");
            mockJsonClient.Setup(c => c.SendJsonTo(documentUri, "{\"_id\":\"someid\",\"type\":\"TestDocument\"}", "PUT")).Returns(expectedJson);

            var json = documentRepository.Create(testDocument);

            mockCouchUriBuilder.Verify(s => s.DocumentUri(testDocument.Id));
            mockJsonClient.Verify(c => c.SendJsonTo(documentUri, "{\"_id\":\"someid\",\"type\":\"TestDocument\"}", "PUT"));
            Assert.That(json, Is.EqualTo(expectedJson) );
        }

        [Test]
        public void UpdateDocumentCallsSendJsonWithPutToDocumentUri()
        {
            const string expectedJson = "{\"ok\":\"true\"}";
            var testDocument = new TestDocument("someid");
            mockJsonClient.Setup(c => c.SendJsonTo(documentUri, testDocument.Serialize(), "PUT")).Returns(expectedJson);

            var json = documentRepository.Update(testDocument);

            mockCouchUriBuilder.Verify(s => s.DocumentUri(testDocument.Id));
            mockJsonClient.Verify(c => c.SendJsonTo(documentUri, testDocument.Serialize(), "PUT"));
            Assert.That(json, Is.EqualTo(expectedJson));
        }

        [Test]
        public void GetDocumentCallsGetJsonFromDocumentUri()
        {
            const string expectedJson = "{\"_id\":\"someid\"}";
            mockJsonClient.Setup(c => c.GetJsonFrom(documentUri)).Returns(expectedJson);

            var testDocument = documentRepository.Get("someid");

            mockCouchUriBuilder.Verify(s => s.DocumentUri("someid"));
            mockJsonClient.Verify(c => c.GetJsonFrom(documentUri));
            Assert.That(testDocument.Id, Is.EqualTo("someid"));
        }

        [Test]
        public void ReturnsTrueWhenDocumentIsFound()
        {
            const string expectedJson = "{\"_id\":\"someid\"}";
            mockJsonClient.Setup(c => c.GetJsonFrom(documentUri)).Returns(expectedJson);

            var hasDocument = documentRepository.HasDocument(new TestDocument("someid"));

            mockCouchUriBuilder.Verify(s => s.DocumentUri("someid"));
            mockJsonClient.Verify(c => c.GetJsonFrom(documentUri));
            Assert.That(hasDocument, Is.EqualTo(true));
        }

        [Test]
        public void ThrowsExceptionWhenItIsNotAHttpNotFoundWebException()
        {
            mockJsonClient.Setup(c => c.GetJsonFrom(documentUri)).Throws<ArgumentException>();
            
            Assert.Throws<ArgumentException>(() => documentRepository.HasDocument(new TestDocument("someid")));
        }
    }
}

