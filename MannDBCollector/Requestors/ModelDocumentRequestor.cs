﻿using DataCollector.WebRequest.Get;
using MannDBCollector.Common.Contracts;

namespace MannDBCollector.Requestors
{
    public class ModelDocumentRequestor : DocumentRequester, IModelDocumentRequestor
    {
        public ModelDocumentRequestor(string url) : base(url) { }
    }
}