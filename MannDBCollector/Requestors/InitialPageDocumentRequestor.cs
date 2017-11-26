using DataCollector.WebRequest.Get;
using MannDBCollector.Common.Contracts;

namespace MannDBCollector.Requestors
{
    public class InitialPageDocumentRequestor : DocumentRequestor, IInitialPageDocumentRequestor
    {
        private VehicleTypes? vehicleType;

        public InitialPageDocumentRequestor(WebRequestor webRequestor, string url) : base(webRequestor, url) { }

        public InitialPageDocumentRequestor(WebRequestor webRequestor, string url, VehicleTypes? vehicleType = null) : base(webRequestor, url)
        {
            this.vehicleType = vehicleType;
        }

        public VehicleTypes? VehicleType
        {
            get
            {
                return this.vehicleType;
            }
        }
    }
}
