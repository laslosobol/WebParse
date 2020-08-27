using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace SampleProject.Backend.ClientBase
{
    public class RequestBase
    {
        protected static string UserAgent =
           "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:72.0) Gecko/20100101 Firefox/72.0";//Or any another useragent you need

        protected static readonly Dictionary<string, string> HeadersDictionary = new Dictionary<string, string>//or any another headers you need to send to destination service
        {
            {"X-Requested-With", "XMLHttpRequest"},
            {"Accept-Encoding", "gzip, deflate, br"},
            {"Accept-Language", "en-US,en;q=0.8,en-US;q=0.5,en;q=0.3"},
            {"Content-Encoding", "amz-1.0"}
        };
        protected static CookieContainer CContainer;
        internal static RestClient Client;
        public static string DestinationUrl { get; set; }
    }
}
