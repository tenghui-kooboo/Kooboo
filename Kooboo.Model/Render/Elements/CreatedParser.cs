using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Dom;

namespace Kooboo.Model.Render.Elements
{
    public class CreatedParser : IVirtualElementParser
    {
        public string Name => "created";

        public string CreatedFunc { get; set; }
        public void Parse(Element el, IJsBuilder js, ViewParseOptions options, Action visitChildren)
        {
            this.CreatedFunc = el.getAttribute(options.GetAttributeName(Name));
        }
    }
}
