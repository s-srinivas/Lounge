using System;
using System.Net;
using System.Text;

namespace Lounge.Core.Json
{
    public class JsonClient
    {
        public const string ContentType = "application/json";

        public virtual string GetJsonFrom(Uri requestUri)
        {
            string stringReturned;
            using (var webclient = new WebClient())
            {
                Console.WriteLine("HTTP GET : {0}", requestUri.AbsoluteUri);
                stringReturned = webclient.DownloadString(requestUri);
            }
            return stringReturned;
        }

        public virtual string SendJsonTo(Uri requestUri, string jsonToSend,  string httpVerb)
        {
            string json;
            using (var client = new WebClient())
            {
                Console.WriteLine("HTTP {0} : {1}", httpVerb, requestUri.AbsoluteUri);
                Console.WriteLine(jsonToSend);
                client.Headers["Content-Type"] = ContentType;
                var response = client.UploadData(requestUri, httpVerb, Encoding.ASCII.GetBytes(jsonToSend));
                json = Encoding.UTF8.GetString(response);
            }
            return json;
        }
    }
}