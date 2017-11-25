using DataCollector.Common.Contracts;
using HtmlAgilityPack;
using System.Text;

namespace MannDBCollector.Parsers
{
    public class FilterPageParser : MannDBHtmlParser<string>
    {
        public FilterPageParser(IDocumentRequestor documentRequester) : base(documentRequester) { }

        public override string Parse()
        {
            var dimensionsNode = FindNode("//div[@class='m-all s-all t-1of3 d-1of3 last cf']");
            if (dimensionsNode == null) return base.Parse();

            var dimensions = ChildrenOf(dimensionsNode, "div/ul[@class='productInformation']/li");
            if (dimensions == null) return base.Parse();

            StringBuilder sb = new StringBuilder();
            foreach (HtmlNode dimensionRow in dimensions)
            {
                var dimensionTitleNode = dimensionRow.SelectSingleNode("h3");
                var dimensionValueNode = dimensionRow.SelectSingleNode("span");
                if (dimensionTitleNode == null || dimensionValueNode == null) continue;

                sb.Append($"{dimensionTitleNode.FirstChild.InnerText.Trim()}{dimensionValueNode.FirstChild.InnerText.Trim()} ");
            }
            return sb.ToString().Trim();
        }
    }
}
