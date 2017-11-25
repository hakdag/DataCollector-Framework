using DataCollector.Common.Contracts;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataCollector.Parsing
{
    public class HtmlParser<T>
    {
        private HtmlDocument document;
        private IDocumentRequestor documentRequestor;

        public HtmlParser(IDocumentRequestor documentRequestor)
        {
            this.documentRequestor = documentRequestor;
            this.document = documentRequestor.GetHtml();
        }

        public HtmlParser(HtmlDocument document)
        {
            this.document = document;
        }

        public virtual T Parse()
        {
            return default(T);
        }

        public HtmlNode FindNode(string xpath) => document.DocumentNode.SelectSingleNode(xpath);

        public HtmlNodeCollection FindNodes(string xpath) => document.DocumentNode.SelectNodes(xpath);

        public string AttributeOf(HtmlNode node, string attrName)
        {
            if (node == null || node.Attributes[attrName] == null) return null;

            return node.Attributes[attrName].Value;
        }

        public HtmlNode ChildOf(HtmlNode parentNode, string xpath)
        {
            if (parentNode == null) return null;

            var node = parentNode.SelectSingleNode(xpath);
            return node;
        }

        public HtmlNodeCollection ChildrenOf(HtmlNode parentNode, string xpath)
        {
            if (parentNode == null) return null;

            var childrenNodes = parentNode.SelectNodes(xpath);
            return childrenNodes;
        }

        public IEnumerable<HtmlNode> ChildrenOf(HtmlNode parentNode, string xpath, Func<HtmlNode, bool> filterCondition)
        {
            if (parentNode == null) return null;

            var childrenNodes = ChildrenOf(parentNode, xpath);
            var filteredNodes = childrenNodes.Where(filterCondition);
            return filteredNodes;
        }

        protected IDocumentRequestor DocumentRequestor
        {
            get
            {
                return documentRequestor;
            }
        }

        public HtmlDocument Document
        {
            get
            {
                return document;
            }

            set
            {
                document = value;
            }
        }
    }
}
