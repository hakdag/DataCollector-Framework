using System.Collections.Generic;

namespace MannDBCollector.Common
{
    public class FilterNodeModel
    {
        public string DetailPageUrl { get; set; }
        public string Filters { get; set; }
        public List<string> FilterUrls { get; set; }
    }
}
