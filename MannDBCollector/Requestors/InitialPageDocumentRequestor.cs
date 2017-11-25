using DataCollector.WebRequest.Get;
using MannDBCollector.Common.Contracts;

namespace MannDBCollector.Requestors
{
    public class InitialPageDocumentRequestor : DocumentRequester, IInitialPageDocumentRequestor
    {
        private VehicleTypes? vehicleType;

        public InitialPageDocumentRequestor(string url) : base(url) { }

        public InitialPageDocumentRequestor(string url, VehicleTypes? vehicleType = null) : base(url)
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
