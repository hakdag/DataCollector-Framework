using DataCollector.WebRequest.Get;
using MannDBCollector.Common.Contracts;

namespace MannDBCollector.Requestors
{
    public class ModelDetailDocumentRequestor : DocumentRequester, IModelDetailDocumentRequestor
    {
        public ModelDetailDocumentRequestor(WebRequestor webRequestor, string url) : base(webRequestor, url)
        {
        }
    }
}
