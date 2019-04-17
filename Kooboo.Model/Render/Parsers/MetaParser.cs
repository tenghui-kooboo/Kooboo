using System;

using Kooboo.Dom;

namespace Kooboo.Model.Render.Parsers
{
    public class MetaParser : IVirtualElementParser
    {
        public string Name => "meta";

        public int Priority => ParserPriority.High;

        public void Parse(Element el, TagParseContext context, Action visitChildren)
        {
            var modelName= el.getAttribute(context.Options.GetAttributeName(Name));

            var metaKey = string.Format("{0}_{1}", modelName, Name);

            context.Js.Data(modelName, "{}");

            string url = $"/meta/get?modelname={modelName}";
            context.Js.Load(url, modelName);

            visitChildren?.Invoke();

        }
    }
}
