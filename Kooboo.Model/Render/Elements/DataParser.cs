using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Dom;

namespace Kooboo.Model.Render.Elements
{
    public class DataParser : IVirtualElementParser
    {
        public string Name => "datafrom";

        public string Api { get; set; }

        public void Parse(Element el, IJsBuilder js, ViewParseOptions options, Action visitChildren)
        {
            this.Api= el.getAttribute(options.GetAttributeName(Name));

            var modelAy = new ApiAnalysize(this.Api,options.ApiProvider);

            var list= modelAy.GetDataList();
            foreach(var item in list)
            {
                js.Data(item.Key, item.Value);
            }

            var rules = modelAy.GetRules();
            foreach (var rule in rules)
            {
                js.Validation(rule.Key, rule.Value);
            }

        }
    }
}
