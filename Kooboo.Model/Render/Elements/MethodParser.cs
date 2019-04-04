using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Dom;

namespace Kooboo.Model.Render.Elements
{
    public class MethodParser : IVirtualElementParser
    {
        public string Name => "api";

        public string Api { get; set; }

        public string Callback { get; set; }

        public void Parse(Element el, IJsBuilder js, ViewParseOptions options, Action visitChildren)
        {
            this.Api= el.getAttribute(options.GetAttributeName(Name));
            this.Callback = el.getAttribute(options.GetAttributeName("callback"));

            var modelAy = new ApiAnalysize(this.Api, options.ApiProvider);
            modelAy.MethodCallBack = Callback;

            js.Method(modelAy.ApiModel.Method, modelAy.GetMethodBody());
            
            
        }
    }
}
