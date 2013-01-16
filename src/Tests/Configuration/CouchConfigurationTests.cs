using System.Collections.Specialized;
using Lounge.Core.Configuration;
using NUnit.Framework;

namespace Lounge.Tests.Configuration
{
    [TestFixture]
    public class CouchConfigurationTests
    {
        private NameValueCollection configurationSettings;

        [SetUp]
        public void BeforeEachTest()
        {
            configurationSettings = new NameValueCollection
            {
                {"CouchServerHost", "somecouchserver"},
                {"CouchServerPort", "1433"},
                {"CouchServerDatabase", "somedatabase"},
                {"CouchServerUserName", ""},
                {"CouchServerPassword", ""}
            };
        }

        [Test]
        public void ServerUriIncludesHostAndPort()
        {
            var couchConfiguration = new CouchConfiguration(configurationSettings);

            Assert.That(couchConfiguration.ServerUri.AbsoluteUri, Is.EqualTo("http://somecouchserver:1433/"));
        }

        [Test]
        public void DatabaseUriIncludesHostAndPort()
        {
            var couchConfiguration = new CouchConfiguration(configurationSettings);

            Assert.That(couchConfiguration.DatabaseUri.AbsoluteUri, Is.EqualTo("http://somecouchserver:1433/somedatabase/"));
        }

        [Test]
        public void DatabaseIsReturnedFromConfigurationSetting()
        {
            var couchConfiguration = new CouchConfiguration(configurationSettings);

            Assert.That(couchConfiguration.DatabaseName, Is.EqualTo("somedatabase"));
        }
    }
}