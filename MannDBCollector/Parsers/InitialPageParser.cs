using DataCollector.Common;
using DataCollector.Common.Contracts;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace MannDBCollector.Parsers
{
    public class InitialPageParser : MannDBHtmlParser<ITitleLinkPair[]>
    {
        public InitialPageParser(IDocumentRequestor documentRequestor) : base(documentRequestor) { }

        public override ITitleLinkPair[] Parse()
        {
            var contentDefaultNode = FindNode("//div[@id='vehicleDataStart']/div[@class='content-default']");
            IEnumerable<HtmlNode> vehiclesButtonNodes = ChildrenOf(contentDefaultNode, "span", n => n.Attributes["class"] != null && n.Attributes["class"].Value.Contains("vehiclesButton"));
            if (vehiclesButtonNodes == null) return new ITitleLinkPair[0];

            List<ITitleLinkPair> list = new List<ITitleLinkPair>();
            foreach (HtmlNode vehiclesButtonNode in vehiclesButtonNodes)
            {
                HtmlNode linkNode = ChildOf(vehiclesButtonNode, "a"); // vehiclesButtonNode.SelectSingleNode("a");
                if (linkNode == null) continue;

                var imgNode = ChildOf(linkNode, "img");
                if (imgNode == null) continue;

                string link = AttributeOf(linkNode, "href");
                string title = AttributeOf(imgNode, "alt");
                if (!String.IsNullOrEmpty(link) && !String.IsNullOrEmpty(title))
                    list.Add(new TitleLinkPair { Link = link, Title = title });
            }

            return list.ToArray();
        }
    }
}
