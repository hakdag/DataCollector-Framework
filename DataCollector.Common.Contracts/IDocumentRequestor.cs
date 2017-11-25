using HtmlAgilityPack;

namespace DataCollector.Common.Contracts
{
    public interface IDocumentRequestor
    {
        HtmlDocument GetHtml();
        HtmlDocument RequestDocument(string url);
    }
}
