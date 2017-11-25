using HtmlAgilityPack;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace DataCollector.WebRequest.Get
{
    public class WebRequestor
    {
        public WebRequestor() { }

        public HtmlDocument GetResponseHtml(string uri, string cacheKey = null, Cookie sessionCookie = null)
        {
            //Program.requestsWriter.Write("Requesting: " + uri);
            return getResponseHtml(uri, sessionCookie);
        }


        private HtmlDocument getResponseHtml(string uri, Cookie sessionCookie = null)
        {
            HttpWebResponse httpWebResponse = null;
            try
            {
                httpWebResponse = getWebResponse(uri, sessionCookie);
            }
            catch (System.Net.Sockets.SocketException)
            {
                throw;
            }
            catch (WebException)
            {
                throw;
            }
            catch (Exception)
            {
                //Program.exceptionsWriter.Write(uri + Environment.NewLine);
                //Program.exceptionsWriter.Write(exception.Message);
                //Console.WriteLine(exception.Message);
                throw;
            }

            var contentType = httpWebResponse.ContentEncoding;
            string html;

            switch (contentType.ToLower())
            {
                case "gzip":
                    using (var gzip = new GZipStream(httpWebResponse.GetResponseStream(), CompressionMode.Decompress))
                    {
                        var sr = new StreamReader(gzip);
                        html = sr.ReadToEnd();
                    }
                    break;
                case "":
                    html = readResponse(httpWebResponse);
                    for (int i = 0; i < 10 && String.IsNullOrEmpty(html); i++)
                    {
                        httpWebResponse = getWebResponse(uri, sessionCookie);
                        html = readResponse(httpWebResponse);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("uri");
            }

            HtmlDocument htmlDocument = CreateHtmlDocument(html);
            return htmlDocument;
        }

        public HtmlDocument CreateHtmlDocument(string html)
        {
            var doc = new HtmlDocument();
            using (TextReader tr = new StringReader(html))
            {
                doc.Load(tr);
            }
            return doc;
        }

        private string readResponse(HttpWebResponse httpWebResponse)
        {
            try
            {
                return (new StreamReader(httpWebResponse.GetResponseStream())).ReadToEnd();
            }
            catch (Exception exc)
            {
                //Program.exceptionsWriter.Write(exc.Message);
                return null;
            }
        }

        private HttpWebResponse getWebResponse(string uri, Cookie sessionCookie = null)
        {
            var wr = (HttpWebRequest)System.Net.WebRequest.Create(uri);

            //wr.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            //wr.Headers.Add("Accept-Encoding", "gzip, deflate");
            //wr.Headers.Add("Accept-Language", "en-us,en;q=0.5");
            //wr.Headers.Add("Connection", "keep-alive");
            //wr.Headers.Add("Host", "abc123.com");
            //wr.Headers.Add("Referer", "http://www.abc123.com");
            //wr.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:15.0) Gecko/20100101 Firefox/15.0.1");

            wr.Timeout = 30000;

            wr.Proxy = null;
            wr.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:15.0) Gecko/20100101 Firefox/15.0.1";
            wr.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            wr.ContentType = "text/html; charset=UTF-8";
            //wr.Host = "abc123.com";
            //wr.Referer = "http://www.abc123.com";
            wr.CookieContainer = new CookieContainer();
            if (sessionCookie != null) wr.CookieContainer.Add(sessionCookie);
            var httpWebResponse = wr.GetResponse() as HttpWebResponse;
            return httpWebResponse;
        }
    }
}
