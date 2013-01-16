using System;
using System.Linq;
using Lounge.Core;
using Lounge.Core.Configuration;
using Lounge.Core.Domain;
using Lounge.Core.Json;
using Lounge.Tests.TestData;
using Moq;
using NUnit.Framework;

namespace Lounge.Tests
{
    [TestFixture]
    public class ViewRepositoryTests
    {
        private readonly Mock<JsonClient> mockJsonClient = new Mock<JsonClient>();
        private Mock<IProvideCouchConfiguration> mockConfiguration;
        private Mock<CouchUriBuilder> mockCouchUriBuilder;
        private ViewRepository<TestDocument> viewRepository;
        private Uri documentUri;

        [SetUp]
        public void BeforeEachTest()
        {
            mockConfiguration = new Mock<IProvideCouchConfiguration>();
            mockCouchUriBuilder = new Mock<CouchUriBuilder>(new object[] {mockConfiguration.Object});
            viewRepository = new ViewRepository<TestDocument>(mockCouchUriBuilder.Object,
                                                                        mockJsonClient.Object);
            documentUri = new Uri("http://somecouchserver:1433/db/someid");
        }
           
        [Test]
        public void GetsListOfDocumentsFromViewByCallingGetJsonForView()
        {
            var viewresult = new ViewResult<TestDocument>
            {
                RowCount = 2, 
                Rows = new[]
                {
                    new ViewResultRow<TestDocument> {Id = "1", Key = "1", Value = new TestDocument("1")}, 
                    new ViewResultRow<TestDocument> { Id = "2", Key = "2", Value = new TestDocument("2") }
                }
            };
            var expectedJson = viewresult.Serialize();
            mockCouchUriBuilder.Setup(s => s.ForView(It.IsAny<string>(), It.IsAny<string>())).Returns(documentUri);
            mockJsonClient.Setup(c => c.GetJsonFrom(documentUri)).Returns(expectedJson);

            var testDocuments = viewRepository.GetDocuments("designdoc", "viewname");

            mockCouchUriBuilder.Verify(s => s.ForView("designdoc", "viewname"));
            mockJsonClient.Verify(c => c.GetJsonFrom(documentUri));
            Assert.That(Enumerable.First<TestDocument>(testDocuments).Id, Is.EqualTo("1"));
            Assert.That(Enumerable.Last<TestDocument>(testDocuments).Id, Is.EqualTo("2"));
        }

        [Test]
        public void GetDocumentByKeyFromViewByCallingGetJsonForView()
        {
            var viewresult = new ViewResult<TestDocument>
            {
                RowCount = 1,
                Rows = new[]{new ViewResultRow<TestDocument> {Id = "1", Key = "1", Value = new TestDocument("1")}}
            };
            var expectedJson = viewresult.Serialize();
            mockCouchUriBuilder.Setup(s => s.ForView(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(documentUri);
            mockJsonClient.Setup(c => c.GetJsonFrom(documentUri)).Returns(expectedJson);

            var testDocument = viewRepository.GetDocument("designdoc", "viewname", "1");

            mockCouchUriBuilder.Verify(s => s.ForView("designdoc", "viewname", "1"));
            mockJsonClient.Verify(c => c.GetJsonFrom(documentUri));
            Assert.That((object) testDocument.Id, Is.EqualTo("1"));
        }
    }
}