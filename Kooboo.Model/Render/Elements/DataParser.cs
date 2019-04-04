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

        public Dictionary<string,string> List { get; set; }
        public Dictionary<string, List<ValidateRules.Rule>> Rules { get; set; }

        public void Parse(Element el, IJsBuilder js, ViewParseOptions options, Action visitChildren)
        {
            this.Api= el.getAttribute(options.GetAttributeName(Name));

            var modelAy = new ApiAnalysize(this.Api,options.ApiProvider);

            List = modelAy.GetDataList();
            foreach(var item in List)
            {
                js.Data(item.Key, item.Value);
            }

            Rules = modelAy.GetRules();
            foreach (var rule in Rules)
            {
                js.Validation(rule.Key, rule.Value);
            }

        }
    }
}
