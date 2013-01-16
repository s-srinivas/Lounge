using System;
using System.Collections.Specialized;

namespace Lounge.Core.Configuration
{
    public class CouchConfiguration: IProvideCouchConfiguration
    {
        public const string IdPropertyName = "_id";
        public const string RevisionPropertyName = "_rev";

        public string DatabaseName
        {
            get { return databaseName; }
        }

        private readonly string host;
        private readonly int port;
        private readonly string databaseName;
        private string userName;
        private string password;
        private readonly Uri serverUri;

        public CouchConfiguration()
        {
            host = "127.0.0.1";
            port = 5984;
            databaseName = "couchlounge";  
        }

        public CouchConfiguration(NameValueCollection configurationSettings)
        {
            host = configurationSettings["CouchServerHost"];
            port = Int32.Parse(configurationSettings["CouchServerPort"]);
            databaseName = configurationSettings["CouchServerDatabase"];
            userName = configurationSettings["CouchServerUserName"];
            password = configurationSettings["CouchServerPassword"];
            serverUri = new UriBuilder("http", host, port).Uri;
        }

        public Uri ServerUri
        {
            get { return serverUri; }
        }

        public Uri DatabaseUri
        {
            get { return new Uri(serverUri, string.Format("{0}/", databaseName)); }
        }

        public enum VOUCHER_STATUS
        {
            AVAILABLE,
            ALLOCATED,
            EXPIRED,
            REDEEMED
        }
    }
}