using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Dom;

namespace Kooboo.Model.Render.Elements
{
    public class RootParser : IVirtualElementParser
    {
        public string Name => "el";

        public string El { get; set; }

        public void Parse(Element el, IJsBuilder vue, ViewParseOptions options, Action visitChildren)
        {
            var id = el.getAttribute("id");
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("root element id is necessary.");
            }
            vue.El($"#{id}");

            //visitChildren();
        }
    }
}
