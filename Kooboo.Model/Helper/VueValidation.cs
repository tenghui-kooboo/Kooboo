using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.ValidateRules;
using Kooboo.Api;
using Kooboo.Model.Helper;

namespace Kooboo.Model.VueModels
{
    public class VueValidationManager
    {

        private string GetValidMethod(string api, int tabCount, IApiProvider ApiProvider)
        {
            tabCount++;
            var sb = new StringBuilder();

            var setting = ModelHelper.GetSetting(api, ApiProvider);
            if (setting != null)
            {
                var fields = RuleHelper.GetVueFields(setting.GetType());
                if (fields.Count > 0)
                {
                    sb.AppendTabs(tabCount, "var self=this");
                    sb.AppendTabs(tabCount, "function isValid(){");

                    var validateBuilder = new StringBuilder();

                    var ruleFields = fields.FindAll(f => f.ValidateRules.Count > 0);
                    foreach (var field in ruleFields)
                    {
                        if (validateBuilder.Length > 0)
                        {
                            validateBuilder.Append("&&");
                        }
                        validateBuilder.AppendFormat("!self.$v.{0}.$invalid", field.Name);
                    }
                    if (validateBuilder.Length > 0)
                    {
                        validateBuilder.Insert(0, "return ");
                        sb.AppendTabs(tabCount + 1, validateBuilder.ToString());
                    }


                    sb.AppendTabs(tabCount, "}");

                    //valid method
                    sb.AppendTabs(tabCount, "if(!isValid()){return ;}");

                }

            }

            return sb.ToString();
        }
    }

}
