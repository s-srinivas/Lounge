using Lounge.Core.Json;
using NUnit.Framework;

namespace Lounge.Tests
{
    [TestFixture]
    public class JsonClientTests
    {
        [Test]
        public void ContentTypeIsJson()
        {
            Assert.That(JsonClient.ContentType, Is.EqualTo("application/json"));
        }
    }
}