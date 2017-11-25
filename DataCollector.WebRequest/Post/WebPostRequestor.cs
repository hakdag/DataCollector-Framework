using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace DataCollector.WebRequest.Post
{
    public class WebPostRequestor
    {
        private PostProperties _properties;

        public WebPostRequestor(PostProperties properties)
        {
            this._properties = properties;
        }

        public HtmlDocument GetPostedResponseHtml(Uri uri, List<KeyValuePair<string, string>> postParams, string cacheKey = null)
        {
            string url = uri.ToString();
            return getPostedResponseHtml(url, postParams);
        }

        public HtmlDocument GetPostedResponseHtml(string url, List<KeyValuePair<string, string>> postParams, string cacheKey = null)
        {
            return getPostedResponseHtml(url, postParams);
        }

        private HtmlDocument getPostedResponseHtml(string formUrl, List<KeyValuePair<string, string>> postParams)
        {
            string poststr = string.Empty;
            for (int i = 0; i < postParams.Count; i++)
            {
                var param = postParams[i];
                poststr += param.Key + "=" + param.Value;
                if (i < postParams.Count - 1)
                {
                    poststr += "&";
                }
            }

            HttpWebRequest req = (HttpWebRequest)System.Net.WebRequest.Create(formUrl);

            req.Proxy = _properties.Proxy ?? null;
            req.UseDefaultCredentials = _properties.UseDefaultCredentials ?? true;
            req.CookieContainer = _properties.CookieContainer ?? new CookieContainer();
            req.AllowAutoRedirect = _properties.AllowAutoRedirect ?? true;
            req.UserAgent = _properties.UserAgent ?? "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.71 Safari/537.36";
            req.Accept = _properties.Accept ?? "*/*";
            req.Host = _properties.Host ?? "www.example.com";
            req.Headers.Add("Faces-Request", _properties.FacesRequest ?? "partial/ajax");
            req.MaximumAutomaticRedirections = _properties.MaximumAutomaticRedirections ?? 100;
            req.ContentType = _properties.ContentType ?? "application/x-www-form-urlencoded;charset=UTF-8";
            req.Method = "POST";

            byte[] bytes = Encoding.ASCII.GetBytes(poststr);
            req.ContentLength = bytes.Length;
            using (Stream os = req.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }
            WebResponse resp = req.GetResponse();
            string html = (new StreamReader(resp.GetResponseStream())).ReadToEnd();
            var doc = new HtmlDocument();
            using (TextReader tr = new StringReader(html))
            {
                doc.Load(tr);
            }
            return doc;
        }
    }
}
