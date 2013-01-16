using Lounge.Core.Domain;
using Lounge.Core.Json;
using NUnit.Framework;

namespace Lounge.Tests.Domain
{
    [TestFixture]
    public class ViewTests
    {
        [Test]
        public void ReduceFunctionIsNullWhenNotSpecified()
        {
            var view = new View("mapFunction");

            Assert.That(view.Reduce, Is.EqualTo(null));
            Assert.That(view.Map, Is.EqualTo("mapFunction"));
        }

        [Test]
        public void MapFunctionIsNullWhenNotSpecified()
        {
            var view = new View(null, "ReduceFunction");

            Assert.That(view.Map, Is.EqualTo(null));
            Assert.That(view.Reduce, Is.EqualTo("ReduceFunction"));
        }

        [Test]
        public void NullMapFunctionIsIgnoredOnSerialization()
        {
            var view = new View(null, "ReduceFunction");

            var json = view.Serialize();

            Assert.That(json, Is.Not.ContainsSubstring("map"));
        }

        [Test]
        public void NullReduceFunctionIsIgnoredOnSerialization()
        {
            var view = new View("MapFunction", null);

            var json = view.Serialize();

            Assert.That(json, Is.Not.ContainsSubstring("reduce"));
        }
    }
}