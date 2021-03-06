﻿using DataCollector.WebRequest.Get;
using MannDBCollector.Common.Contracts;

namespace MannDBCollector.Requestors
{
    public class ModelPageDocumentRequestor : DocumentRequestor, IModelPageDocumentRequestor
    {
        public ModelPageDocumentRequestor(WebRequestor webRequestor, string url) : base(webRequestor, url) { }
    }
}
