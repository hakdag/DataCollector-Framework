using DataCollector.Common;
using DataCollector.Common.Contracts;
using DataCollector.WebRequest.Exceptions;
using HtmlAgilityPack;
using System;
using System.Net;

namespace DataCollector.WebRequest.Get
{
    public class DocumentRequester : IDocumentRequestor
    {
        public RequestStates State { get; set; } = RequestStates.Ready;
        public string FailMessage { get; set; }

        private readonly IWebRequestor webRequestor;
        private string url;
        private readonly bool persistStatus200;
        private readonly Cookie sessionCookie;

        #region Constructors
        public DocumentRequester(IWebRequestor webRequestor, string url)
        {
            this.webRequestor = webRequestor;
            this.url = url;
            validate();
        }

        public DocumentRequester(IWebRequestor webRequestor, string url, Cookie sessionCookie)
        {
            this.webRequestor = webRequestor;
            this.url = url;
            this.sessionCookie = sessionCookie ?? throw new ArgumentNullException(nameof(sessionCookie));
            validate();
        }

        public DocumentRequester(IWebRequestor webRequestor, string url, bool persistStatus200)
        {
            this.webRequestor = webRequestor;
            this.url = url;
            this.persistStatus200 = persistStatus200;
            validate();
        }

        public DocumentRequester(IWebRequestor webRequestor, string url, Cookie sessionCookie, bool persistStatus200)
        {
            this.url = url;
            this.sessionCookie = sessionCookie ?? throw new ArgumentNullException(nameof(sessionCookie));
            this.persistStatus200 = persistStatus200;
            validate();
        }
        #endregion

        public virtual HtmlDocument GetHtml()
        {
            HtmlDocument htmlDocument = null;
            try
            {
                htmlDocument = webRequestor.GetResponseHtml(this.url, this.sessionCookie, this.persistStatus200);
                State = RequestStates.Requested;
            }
            catch (Exception exc)
            {
                FailMessage = exc.Message;
                State = RequestStates.Failed;
            }

            return htmlDocument;
        }

        public virtual HtmlDocument RequestDocument(string url)
        {
            HtmlDocument htmlDocument = null;

            try
            {
                htmlDocument = webRequestor.GetResponseHtml(url);
                State = RequestStates.Requested;
            }
            catch (Exception exc)
            {
                FailMessage = exc.Message;
                State = RequestStates.Failed;
            }

            return htmlDocument;
        }

        private void validate()
        {
            if (String.IsNullOrEmpty(this.url))
                throw new InvalidURLException();
        }
    }
}
