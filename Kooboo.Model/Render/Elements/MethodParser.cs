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
            js.Method(modelAy.ApiModel.Api, GetMethodBody(modelAy));
        }

        public string GetMethodBody(ApiAnalysize modelAy)
        {
            var rules = modelAy.GetRules();

            StringBuilder sb = new StringBuilder();
            //sb.Append($"{MethodName}:function(){{");
            sb.AppendLine().Append("    var self=this;");
            #region valid method
            sb.AppendLine().Append("    function isValid(){");

            var triggerValid = new StringBuilder();
            var validCondition = new StringBuilder();
            var index = 0;
            foreach (var rule in rules)
            {
                if (index > 0)
                {
                    triggerValid.Append("||");
                    validCondition.Append("&&");
                }
                triggerValid.AppendFormat("self.$v.{0}.$touch()", rule.Key);
                validCondition.AppendFormat("!self.$v.{0}.$invalid", rule.Key);
                index++;
            }
            sb.AppendLine().Append("    " + triggerValid.ToString());
            sb.AppendLine().AppendFormat("      return {0}", validCondition.ToString());

            sb.AppendLine().Append("    }");
            sb.AppendLine().Append("    if(!isValid()){return ;}");
            #endregion

            sb.AppendLine().AppendFormat("      {0}({{", modelAy.ApiModel.GetApi());
            var datalist = modelAy.GetDataList();
            var i = 0;
            foreach (var item in datalist)
            {
                if (i > 0)
                {
                    sb.Append(",");
                }
                sb.AppendLine().AppendFormat("      {0}:self.$data.{0}", item.Key);
                i++;
            }
            //if call back is empty ,auto refresh url;
            var callback = string.IsNullOrEmpty(this.Callback) ?
                        "if(res.success){location.href=res.model} " :
                        $"self.{this.Callback}(res);";

            sb.AppendLine().Append($"   }}).then(function(res){{ {callback} }})");

            //sb.Append("}");

            return sb.ToString();
        }


    }
}
