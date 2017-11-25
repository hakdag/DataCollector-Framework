using DataCollector.Common;
using MannDBCollector.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannDBCollector.Common
{
    public class VehicleTypeCollectorSet : CollectorSet
    {
        public string Data { get; set; }
    }

    public class ProducerCollectorSet : CollectorSet
    {
        public string Data { get; set; }
    }

    public class ModelCollectorSet : CollectorSet
    {
        public string Data { get; set; }
    }

    public class ModelPageCollectorSet : CollectorSet
    {
        public IModelDetailDocumentRequestor Data { get; set; }
    }

    public class ModelPageDetailCollectorSet : CollectorSet
    {
        public RowModel Data { get; set; }
    }
}
