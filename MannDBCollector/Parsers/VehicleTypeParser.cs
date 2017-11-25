using DataCollector.Common;
using DataCollector.Common.Contracts;
using HtmlAgilityPack;
using MannDBCollector.Common;
using System.Linq;

namespace MannDBCollector.Parsers
{
    public class VehicleTypeParser : MannDBHtmlParser<ITitleLinkPair[]>
    {
        public VehicleTypeParser(IDocumentRequestor documentRequestor) : base(documentRequestor) { }

        public override ITitleLinkPair[] Parse()
        {
            var ureticiSelectScriptNode = FindNode("//*[@id='dropdownsHerstellerSelect']/script");
            string ureticiSelectScript = getUreticiSelectScript(ureticiSelectScriptNode);
            ModelSelect[] ureticiSelects = SerializeModelSelects(ureticiSelectScript);
            return ureticiSelects.Select(u => new TitleLinkPair { Link = u.value, Title = u.label }).ToArray();
        }

        private string getUreticiSelectScript(HtmlNode ureticiSelectScriptNode)
        {
            string selectScript = ureticiSelectScriptNode.InnerText.Trim();
            selectScript = selectScript.Substring("new RichFaces.ui.Select(\"dropdownsHerstellerSelect\", {\"clientSelectItems\":".Length);
            int removeFromEnd = selectScript.IndexOf("itemCss") - 3;
            selectScript = selectScript.Substring(0, removeFromEnd);
            return selectScript;
        }
    }
}
