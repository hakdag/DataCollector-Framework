using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.Common
{
    public class CollectorSet
    {
        public string Name { get; set; }
        // public abstract T Data { get; set; }
        public CollectorSet[] Subsets { get; set; }
        public CollectorSet Parent { get; set; }
    }
}
