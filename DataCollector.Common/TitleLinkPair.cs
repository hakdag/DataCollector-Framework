using DataCollector.Common.Contracts;
using System;

namespace DataCollector.Common
{
    [Serializable]
    public class TitleLinkPair : ITitleLinkPair
    {
        public string Title { get; set; }
        public string Link { get; set; }
    }
}
