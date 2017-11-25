using DataCollector.Common.Contracts;
using System.Linq;

namespace MannDBCollector.Parsers
{
    public class ModelPageParser : MannDBHtmlParser<string[]>
    {
        public ModelPageParser(IDocumentRequestor documentRequester) : base(documentRequester) { }

        public override string[] Parse()
        {
            var rowNodes = FindNodes("//*[@class='row clickable_frame']");
            return rowNodes.Select(rowNode => parseModelRowOnclick(AttributeOf(rowNode, "onclick"))).ToArray();
        }

        private string parseModelRowOnclick(string onclick)
        {
            onclick = onclick.Trim().Substring("location.href=".Length).Trim('\'').Replace("&gt;", ">").Replace("&quot;", "\"");
            return onclick;
        }
    }
}
