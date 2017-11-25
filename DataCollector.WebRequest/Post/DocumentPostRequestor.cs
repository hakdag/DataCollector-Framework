using DataCollector.Common;
using DataCollector.Common.Contracts;
using DataCollector.WebRequest.Exceptions;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace DataCollector.WebRequest.Post
{
    public class DocumentPostRequestor : IDocumentRequestor
    {
        public RequestStates State { get; set; } = RequestStates.Ready;
        public string FailMessage { get; set; }

        private string url;
        private PostProperties properties;
        private List<KeyValuePair<string, string>> postParams;

        public DocumentPostRequestor(string url, PostProperties properties, List<KeyValuePair<string, string>> postParams)
        {
            this.url = url;
            this.properties = properties ?? new PostProperties();
            this.postParams = postParams ?? new List<KeyValuePair<string, string>>();
            validate();
        }

        public HtmlDocument GetHtml()
        {
            try
            {
                WebPostRequestor webPostRequestor = new WebPostRequestor(properties);
                HtmlDocument htmlDocument = webPostRequestor.GetPostedResponseHtml(this.url, postParams);
                State = RequestStates.Requested;
                return htmlDocument;
            }
            catch (Exception exc)
            {
                FailMessage = exc.Message;
                State = RequestStates.Failed;

                throw;
            }
        }

        private void validate()
        {
            if (String.IsNullOrEmpty(this.url))
                throw new InvalidURLException();
        }

        public HtmlDocument RequestDocument(string url)
        {
            throw new NotImplementedException();
        }
    }
}
