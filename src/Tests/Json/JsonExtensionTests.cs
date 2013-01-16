using Lounge.Core.Json;
using NUnit.Framework;
using Newtonsoft.Json;

namespace Lounge.Tests.Json
{
    [TestFixture]
    public class JsonExtensionTests
    {
        [Test]
        public void RemovesRevisionPropertyFromJson()
        {
            const string expectedJson = "{\"_id\":\"1\"}";
            const string json = "{\"_id\": \"1\",\"_rev\": \"1\"}";

            var actualJson = json.RemoveRevisionProperty();

            Assert.That(actualJson, Is.EqualTo(expectedJson));
        }

        [Test]
        public void ReturnsJsonAsIsWhenRevisionPropertyIsNotPresent()
        {
            const string expectedJson = "{\"_id\":\"1\"}";

            var actualJson = expectedJson.RemoveRevisionProperty();

            Assert.That(actualJson, Is.EqualTo(expectedJson));
        }

        [Test]
        public void ThrowsJsonReaderExceptionWhenJsonIsInvalid()
        {
            const string json = "invalidJson";

            Assert.Throws<JsonReaderException>(() => json.RemoveRevisionProperty());
        }
    }
}
