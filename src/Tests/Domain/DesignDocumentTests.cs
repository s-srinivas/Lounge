using Lounge.Core.Domain;
using Lounge.Tests.TestData;
using NUnit.Framework;

namespace Lounge.Tests.Domain
{
    [TestFixture]
    public class DesignDocumentTests
    {
        [Test]
        public void IdIsInitialisedUsingDesignDocumentName()
        {
            var testDesignDocument = new TestDesignDocument("Customer");

            Assert.That(testDesignDocument.Id, Is.EqualTo("_design/Customer"));
            Assert.That(testDesignDocument.Views.Count, Is.EqualTo(0));
        }

        [Test]
        public void DesignDocumentLanguageIsJavascriptByDefault()
        {
            var testDesignDocument = new TestDesignDocument("Customer");

            Assert.That(testDesignDocument.Language, Is.EqualTo("javascript"));
        }

        [Test]
        public void CanAddAViewToDesignDocument()
        {
            var testDesignDocument = new TestDesignDocument("Customer");
            var someView = new View();

            testDesignDocument.AddView("test", someView);

            Assert.That(testDesignDocument.Views["test"], Is.EqualTo(someView));
        }
    }
}