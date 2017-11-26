using DataCollector.WebRequest.Get;
using MannDBCollector.Common.Contracts;

namespace MannDBCollector.Requestors
{
    public class FilterPageDocumentRequestor : DocumentRequestor, IFilterPageDocumentRequestor
    {
        public FilterPageDocumentRequestor(WebRequestor webRequestor, string url) : base(webRequestor, url) { }
    }
}
