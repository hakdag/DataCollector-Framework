using DataCollector.WebRequest.Get;
using MannDBCollector.Common.Contracts;

namespace MannDBCollector.Requestors
{
    public class VehicleTypeDocumentRequestor : DocumentRequester, IVehicleTypeDocumentRequestor
    {
        public VehicleTypeDocumentRequestor(WebRequestor webRequestor, string url) : base(webRequestor, url) { }
    }
}
