using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.WebRequest.Exceptions
{
    public class WebRequestException : Exception
    {
        public WebRequestException() : base() { }

        public WebRequestException(string message) : base(message) { }
    }
}
