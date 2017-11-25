using DataCollector.WebRequest.Get;
using MannDBCollector.Common.Contracts;

namespace MannDBCollector.Requestors
{
    public class ModelDetailDocumentRequestor : DocumentRequester, IModelDetailDocumentRequestor
    {
        public ModelDetailDocumentRequestor(string url) : base(url)
        {
        }
    }
}
