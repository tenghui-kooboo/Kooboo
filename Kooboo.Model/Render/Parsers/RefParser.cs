using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Kooboo.Dom;

namespace Kooboo.Model.Render.Parsers
{
    public class RefParser : IVirtualElementParser
    {
        public string Name => "ref";

        public int Priority => ParserPriority.High;

        public void Parse(Element el, TagParseContext context, Action visitChildren)
        {
            var viewName = el.getAttribute(context.Options.GetAttributeName(Name));
            var html = context.ViewContext.ViewProvider.GetView(viewName);

            var parser = new ViewParser(context.Options);
            var js = parser.RenderSubView(html);

            context.Js.Component(viewName, js);
        }
    }
}
