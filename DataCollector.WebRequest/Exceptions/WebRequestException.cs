using System;

namespace DataCollector.WebRequest.Exceptions
{
    public class WebRequestException : Exception
    {
        public WebRequestException() : base() { }

        public WebRequestException(string message) : base(message) { }
    }
}
