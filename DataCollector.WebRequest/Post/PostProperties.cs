using System.Net;

namespace DataCollector.WebRequest.Post
{
    public class PostProperties
    {
        public string Accept { get; set; }
        public bool? AllowAutoRedirect { get; set; }
        public string ContentType { get; set; }
        public CookieContainer CookieContainer { get; set; }
        public string FacesRequest { get; set; }
        public string Host { get; set; }
        public int? MaximumAutomaticRedirections { get; set; }
        public IWebProxy Proxy { get; set; }
        public bool? UseDefaultCredentials { get; set; }
        public string UserAgent { get; set; }
    }
}
