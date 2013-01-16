using Lounge.Core.Json;
using Lounge.Tests.TestData;
using NUnit.Framework;

namespace Lounge.Tests.Json
{
    [TestFixture]
    public class SerializerExtensionsTests
    { 

        [Test]
        public void DeserializesTestDataObject()
        {
            var result = "{Name: 'Test'}".Deserialize<TestSerializableClass>();

            Assert.That(result.Name, Is.EqualTo("Test"));
        }

        [Test]
        public void ShouldSerialiseTestData()
        {
            var testData = new TestSerializableClass {Name = "Test"};

            var json = testData.Serialize();

            Assert.That(json, Is.EqualTo("{\"Name\":\"Test\"}"));
        }

        [Test]
        public void SerializingANullStringShouldReturnAnEmptyString()
        {
            TestSerializableClass testSerializableClass = null;

            var actual = testSerializableClass.Serialize();

            Assert.That(actual, Is.EqualTo(string.Empty));
        }
    }
}
