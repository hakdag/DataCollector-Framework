using HtmlAgilityPack;
using System.Net;

namespace DataCollector.Common.Contracts
{
    public interface IWebRequestor
    {
        HtmlDocument GetResponseHtml(string uri, Cookie sessionCookie = null, bool persistStatus200 = false);
        HtmlDocument CreateHtmlDocument(string html);
    }
}
