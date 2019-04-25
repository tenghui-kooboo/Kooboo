using System;
using Kooboo.Dom;
using System.Reflection;
using System.Linq;

namespace Kooboo.Model.Render.Parsers
{
    public class MetaParser : IVirtualElementParser
    {
        private Func<string, bool> _isValidModelName;

        public MetaParser()
            : this(o => Meta.MetaProvider.Instance.IsValidModelName(o))
        {
        }

        public MetaParser(Func<string, bool> isValidModelName)
        {
            _isValidModelName = isValidModelName;
        }

        public string Name => "meta";

        public int Priority => ParserPriority.High;

        public void Parse(Element el, TagParseContext context, Action visitChildren)
        {
            var name = el.getAttribute(context.Options.GetAttributeName(Name));
            if (String.IsNullOrEmpty(name))
            {
                // Get from url
                name = context.ViewContext.Context.Request.QueryString["meta"];
            }

            if (String.IsNullOrEmpty(name))
                throw new ViewParseException($"Can not resolve meta name");

            if (!_isValidModelName(name))
                throw new ViewParseException($"Meta name {name} is never configured");

            el.setAttribute("meta", name);
        }
    }
}
