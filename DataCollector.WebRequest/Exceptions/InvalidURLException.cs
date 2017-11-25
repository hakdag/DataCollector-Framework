using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.WebRequest.Exceptions
{
    public class InvalidURLException : WebRequestException
    {
        public InvalidURLException() : base() { }

        public InvalidURLException(string message) : base(message) { }
    }
}
