using DataCollector.Common.Contracts;
using HtmlAgilityPack;
using MannDBCollector.Common;

namespace MannDBCollector.Parsers
{
    public class ModelDetailParser : MannDBHtmlParser<ModelPageModel>
    {
        public ModelDetailParser(IDocumentRequestor documentRequester) : base(documentRequester) { }

        public override ModelPageModel Parse()
        {
            ModelPageModel model = new ModelPageModel();

            HtmlNode vehicleTableNode = FindNode("//*[@class='table vehicleTable']");
            if (vehicleTableNode != null)
            {
                HtmlNode clickableFrame = ChildOf(vehicleTableNode, "div[@class='row clickable_frame']");
                if (clickableFrame != null)
                {
                    model.DetailPageUrl = parseUrl(clickableFrame);
                    HtmlNodeCollection detailColumns = ChildrenOf(clickableFrame, "div");
                    if (detailColumns != null)
                    {
                        model.ModelType = parseDetailColumn(detailColumns, 0);
                        model.MotorCode = parseDetailColumn(detailColumns, 1);
                        model.kW = parseDetailColumn(detailColumns, 2);
                        model.PS = parseDetailColumn(detailColumns, 3);
                        model.Year = parseDetailColumn(detailColumns, 4);
                    }
                }
            }

            return model;
        }

        private string parseDetailColumn(HtmlNodeCollection detailColumns, int index)
        {
            if (detailColumns.Count < index + 1) return "";

            HtmlNode detailColumn = detailColumns[index];
            if (detailColumn == null) return "";

            var detailColumnInner = detailColumn.SelectSingleNode("div");
            if (detailColumnInner == null) return "";

            return detailColumnInner.InnerText.Trim().Replace("\t", "").Replace("\n", " ");
        }

        private string parseUrl(HtmlNode vehicleTableNode)
        {
            string onclick = AttributeOf(vehicleTableNode, "onclick");
            if (onclick == null) return "";
            onclick = onclick.Trim().Substring("location.href=".Length).Trim('\'').Replace("&gt;", ">").Replace("&quot;", "\"");
            return onclick;
        }
    }
}
