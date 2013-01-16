using System;
using Lounge.Tests.TestData;
using NUnit.Framework;

namespace Lounge.Tests.Domain
{
    [TestFixture]
    public class DocumentTests
    {
       [Test]
        public void UniqueIdIsAssignedForANewDocument()
        {
           var document = new TestDocument();

           Guid actual;
           var isGuid = Guid.TryParse(document.Id, out actual);

           Assert.That(isGuid, Is.True);
        }

        [Test]
        public void DocumentTypeIsSubClassNameForANewDocument()
        {
            var document = new TestDocument();
            
            Assert.That(document.DocumentType, Is.EqualTo("TestDocument"));
        }

        [Test]
        public void DocumentsWithTheSameIdAndRevisionAreEqual()
        {
            var guid = Guid.NewGuid().ToString();
            var document1 = new TestDocument(guid) {Revision = "1"};
            var document2 = new TestDocument(guid) {Revision = "1"};

            var documentsAreEqual = document1.Equals(document2);

            Assert.That(documentsAreEqual, Is.True);
        }

        [Test]
        public void ReturnsFalseWhenOneDocumentIsNullWhenEqualityIsChecked()
        {
            var guid = Guid.NewGuid().ToString();
            var document1 = new TestDocument(guid) { Revision = "1" };

            var documentsAreEqual = document1.Equals(null);

            Assert.That(documentsAreEqual, Is.False);
        }

        [Test]
        public void DocumentsWithTheSameIdAndDifferentRevisionsAreNotEqual()
        {
            var guid = Guid.NewGuid().ToString();
            var document1 = new TestDocument(guid) { Revision = "1" };
            var document2 = new TestDocument(guid) { Revision = "2" };

            var documentsAreEqual = document1.Equals(document2);

            Assert.That(documentsAreEqual, Is.False);
        }

        [Test]
        public void HashCodeIsACombinationOfTheHashcodeAndRevision()
        {
            var guid = Guid.NewGuid().ToString();
            int expectedHashCode = (guid + "1").GetHashCode();
            var document1 = new TestDocument(guid) { Revision = "1" };

            var actualHashCode = document1.GetHashCode();

            Assert.That(actualHashCode, Is.EqualTo(expectedHashCode));
        }
    }
}
