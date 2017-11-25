using DataCollector.Common;
using DataCollector.Common.Contracts;
using HtmlAgilityPack;
using MannDBCollector.Common;
using System.Linq;

namespace MannDBCollector.Parsers
{
    public class ModelParser : MannDBHtmlParser<ITitleLinkPair[]>
    {
        public ModelParser(IDocumentRequestor documentRequester) : base(documentRequester) { }

        public override ITitleLinkPair[] Parse()
        {
            var modelSelectScriptNode = FindNode("//*[@id='dropdownsModellreihe_Select']/script");
            string modelSelectScript = getModelSelectScript(modelSelectScriptNode);
            ModelSelect[] modelSelects = SerializeModelSelects(modelSelectScript);
            return modelSelects.Select(m => new TitleLinkPair { Link = m.value, Title = m.label }).ToArray();
        }

        private static string getModelSelectScript(HtmlNode modelSelectScriptNode)
        {
            string modelSelectScript = modelSelectScriptNode.InnerText.Trim();
            modelSelectScript = modelSelectScript.Substring("new RichFaces.ui.Select(\"dropdownsModellreihe_Select\", {\"clientSelectItems\":".Length);
            int removeFromEnd = modelSelectScript.IndexOf("itemCss") - 3;
            modelSelectScript = modelSelectScript.Substring(0, removeFromEnd);
            return modelSelectScript;
        }
    }
}
