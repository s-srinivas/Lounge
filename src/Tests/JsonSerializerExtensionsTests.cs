using System;
using Lounge.Core.Json;
using NUnit.Framework;

namespace Lounge.Tests
{
    [TestFixture]
    public class JsonSerializerExtensionsTests
    {
        [Serializable]
        public class TestData
        {
            public string Name { get; set; }
        }

        [Test]
        public void DeserializesTestDataObject()
        {
            var result = "{Name: 'Test'}".Deserialize<TestData>();

            Assert.That(result.Name, Is.EqualTo("Test"));
        }

        [Test]
        public void ShouldSerialiseTestData()
        {
            var testData = new TestData {Name = "Test"};

            var json = testData.Serialize();

            Assert.That(json, Is.EqualTo("{\"Name\":\"Test\"}"));
        }
    }
}
