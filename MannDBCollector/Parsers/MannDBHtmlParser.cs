using DataCollector.Common.Contracts;
using DataCollector.Parsing;
using MannDBCollector.Common;
using Newtonsoft.Json;

namespace MannDBCollector.Parsers
{
    public class MannDBHtmlParser<T> : HtmlParser<T>
    {
        public MannDBHtmlParser(IDocumentRequestor documentRequester) : base(documentRequester) { }

        public ModelSelect[] SerializeModelSelects(string modelSelectScript) => JsonConvert.DeserializeObject<ModelSelect[]>(modelSelectScript);
    }
}
