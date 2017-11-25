using DataCollector.Common;
using DataCollector.Common.Contracts;
using DataCollector.WebRequest.Exceptions;
using HtmlAgilityPack;
using System;

namespace DataCollector.WebRequest
{
    public class DocumentRequester : IDocumentRequestor
    {
        public RequestStates State { get; set; } = RequestStates.Ready;
        public string FailMessage { get; set; }

        private string url;

        public DocumentRequester(string url)
        {
            this.url = url;
            validate();
        }

        public virtual HtmlDocument GetHtml()
        {
            try
            {
                WebRequestor webRequestor = new WebRequestor();
                HtmlDocument htmlDocument = webRequestor.GetResponseHtml(this.url);
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

        public virtual HtmlDocument RequestDocument(string url)
        {
            try
            {
                WebRequestor webRequestor = new WebRequestor();
                HtmlDocument htmlDocument = webRequestor.GetResponseHtml(url);
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
    }
}
