using DataCollector.WebRequest.Get;
using MannDBCollector.Common.Contracts;

namespace MannDBCollector.Requestors
{
    public class FilterPageDocumentRequestor : DocumentRequester, IFilterPageDocumentRequestor
    {
        public FilterPageDocumentRequestor(string url) : base(url) { }
    }
}
