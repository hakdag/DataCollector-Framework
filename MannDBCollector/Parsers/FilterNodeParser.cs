using DataCollector.Common.Contracts;
using HtmlAgilityPack;
using MannDBCollector.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MannDBCollector.Parsers
{
    public class FilterNodeParser : MannDBHtmlParser<FilterNodeModel>
    {
        private string filterNodeSelector;
        private string seperator = "\n";

        public FilterNodeParser(IDocumentRequestor documentRequester) : base(documentRequester) { }

        public FilterNodeParser(IDocumentRequestor documentRequester, FilterTypes filterType) : base(documentRequester)
        {
            switch (filterType)
            {
                case FilterTypes.Air:
                    filterNodeSelector = $"div[@class='partRow parts_airfilter']";
                    break;
                case FilterTypes.Oil:
                    filterNodeSelector = $"div[@class='partRow parts_oilFilter']";
                    break;
                case FilterTypes.Fuel:
                    filterNodeSelector = $"div[@class='partRow parts_fuelFilter']";
                    break;
                case FilterTypes.Interior:
                    filterNodeSelector = $"div[@class='partRow parts_cabinAisFilter']";
                    break;
                case FilterTypes.Other:
                    filterNodeSelector = $"div[@class='partRow parts_otherFilter']";
                    break;
            }

        }

        public override FilterNodeModel Parse()
        {
            var partsTableNode = FindNode("//*[@class='partsTable']");
            FilterNodeModel filterNode = parseFilterNode(partsTableNode);
            if (filterNode == null) return base.Parse();
            return filterNode;
        }

        private FilterNodeModel parseFilterNode(HtmlNode partsTableNode)
        {
            if (partsTableNode == null) return null;

            var filterNode = ChildOf(partsTableNode, filterNodeSelector);
            if (filterNode == null) return null;

            List<string> filters = getFiltersForFilterNode(filterNode);
            List<string> filterUrls = parseFilterUrls(filterNode);
            return new FilterNodeModel
            {
                Filters = string.Join(seperator, filters),
                FilterUrls = filterUrls
            };
        }

        private List<string> parseFilterUrls(HtmlNode filterNode)
        {
            List<string> list = new List<string>();

            var columnNodes = ChildrenOf(filterNode, "div[@class='column']");
            if (columnNodes == null) return list;

            foreach (var columnNode in columnNodes)
            {
                var partTitleNode = ChildOf(columnNode, "div/div/div[@class='partsInlineText partsText']");
                if (partTitleNode == null) continue;

                var linkNode = ChildOf(partTitleNode, "a");
                if (linkNode == null) continue;

                var url = AttributeOf(linkNode, "href");
                if (!String.IsNullOrEmpty(url))
                    list.Add(url);
            }

            return list;
        }

        private List<string> getFiltersForFilterNode(HtmlNode filterNode)
        {
            List<string> filters = new List<string>();

            var columnNodes = ChildrenOf(filterNode, "div[@class='column']");
            if (columnNodes == null) return filters;

            foreach (var columnNode in columnNodes)
            {
                var partTitleNode = ChildOf(columnNode, "div/div/div[@class='partsInlineText partsText']");
                if (partTitleNode == null) continue;

                StringBuilder sb = new StringBuilder();
                sb.Append(partTitleNode.InnerText.Trim());
                var partAccessorieTextNode = ChildOf(columnNode, "div/div/div/div/div[@class='partAccessorieText tooltip']");
                if (partAccessorieTextNode != null)
                    sb.Append(" ").Append(partAccessorieTextNode.InnerText.Trim());
                filters.Add(sb.ToString());
            }

            return filters.Where(f => !String.IsNullOrEmpty(f)).ToList();
        }
    }
}
