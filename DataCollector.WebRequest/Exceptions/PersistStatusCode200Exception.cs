using System;

namespace DataCollector.WebRequest.Exceptions
{
    public class PersistStatusCode200Exception : Exception
    {
        public PersistStatusCode200Exception() : base() { }

        public PersistStatusCode200Exception(string message) : base(message) { }
    }
}
