using DataCollector.WebRequest.Get;
using MannDBCollector.Common.Contracts;

namespace MannDBCollector.Requestors
{
    public class ModelPageDocumentRequestor : DocumentRequester, IModelPageDocumentRequestor
    {
        public ModelPageDocumentRequestor(string url) : base(url) { }
    }
}
