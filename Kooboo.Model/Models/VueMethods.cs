using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.ValidateRules;
using Kooboo.Model.Setting;
using System.Reflection;
using Kooboo.Api;
using Kooboo.Model.Helper;

namespace Kooboo.Model
{
    public class VueMethod
    {
        public string Name { get; set; }

        public string Api { get; set; }

        public string CallbackName { get; set; }

        public IApiProvider ApiProvider { get; set; }

        public int TabCount { get; set; }

        public string GetJs()
        {
            //todo add validate

            var sb = new StringBuilder();
            sb.AppendTabs(TabCount,string.Format("{0}:function(){{", Name));

            sb.Append(GetValidMethod(Api,TabCount));
            sb.Append(GetApiRequest(Api, CallbackName, TabCount));

            sb.AppendTabs(TabCount,"}");
            return sb.ToString();
        }

        private string GetValidMethod(string api,int tabCount)
        {
            tabCount++;
            var sb = new StringBuilder();

            var ruleSetting = RuleHelper.GetRuleSettingByApi(api, ApiProvider);
            if(ruleSetting!=null)
            {
                var fields = RuleHelper.GetVueFields(ruleSetting.GetType());
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
                        validateBuilder.AppendFormat("self.$data.{0}.isValid", field.Name);
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

        //kooboo.user.login({username:1,password:2}).then(function(res))
        private string GetApiRequest(string api,string callBackName,int tabCount)
        {
            tabCount++;
            var sb = new StringBuilder();

            sb.AppendTabs(tabCount, api + "({");
            sb.Append(GetApiData(api,tabCount));

            //callback 
            sb.AppendTabs(tabCount, "}).then(function(res){");

            if (!string.IsNullOrEmpty(CallbackName))
            {
                sb.AppendTabs(tabCount + 1, string.Format("{0}(res);", CallbackName));
            }
            
            sb.AppendTabs(tabCount,"})");

            return sb.ToString();
        }

        private string GetApiData(string api,int tabCount)
        {
            tabCount++;
            var sb = new StringBuilder();
            
            var ruleSetting = RuleHelper.GetRuleSettingByApi(Api, ApiProvider);
            if(ruleSetting!=null)
            {
                var fields = RuleHelper.GetVueFields(ruleSetting.GetType());
                var dataBuilder = new StringBuilder();

                foreach (var field in fields)
                {
                    dataBuilder.AppendTabs(tabCount,string.Format("{0}:self.$data.{0}.value,", field.Name));
                }
                sb.Append(dataBuilder.ToString());
            }

            return sb.ToString();
        }

    }

}
