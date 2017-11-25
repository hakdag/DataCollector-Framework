using DataCollector.Common.Contracts;

namespace MannDBCollector.Common.Contracts
{
    public interface IInitialPageDocumentRequestor : IDocumentRequestor
    {
        VehicleTypes? VehicleType { get; }
    }
}
