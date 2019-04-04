using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Api;
using Kooboo.Model.Setting;
using Kooboo.Model.ValidateRules;
using System.Reflection;
using Kooboo.Model.Render.Model;

namespace Kooboo.Model.Render
{
    public class ApiAnalysize
    {
        private KoobooSetting _setting;
        public ApiModel ApiModel;

        public string MethodCallBack { get; set; }
        public ApiAnalysize(string api,IApiProvider provider)
        {
            ApiModel = new ApiModel(api, provider);
            _setting = GetSetting();
        }
        public Dictionary<string, string> GetDataList()
        {
            var dic = new Dictionary<string, string>();
            if (_setting == null) return dic;

            var props = _setting.GetType().GetProperties();
            var attrPropers = typeof(Attribute).GetProperties();

            if (props != null)
            {
                foreach (var prop in props)
                {
                    //exclude attribute props
                    var isAttrProp = attrPropers.ToList().Exists(a => a.Name == prop.Name);
                    if (isAttrProp) continue;
                    string value = "null";
                    if (prop.PropertyType.IsValueType)
                    {
                        value = Activator.CreateInstance(prop.PropertyType).ToString().ToLower();
                    }

                    dic.Add(prop.Name, value);
                }
            }

            return dic;
        }

        public Dictionary<string, List<Rule>> GetRules()
        {
            var dic = new Dictionary<string, List<Rule>>();
            if (_setting == null) return dic;
            var props = _setting.GetType().GetProperties();
            if (props != null)
            {
                foreach (var prop in props)
                {
                    var rules = prop.GetCustomAttributes(typeof(Rule));
                    if (rules != null && rules.Count() > 0)
                    {
                        var validateRules = rules.ToList().Select(r => r as Rule).ToList();
                        dic.Add(prop.Name, validateRules);
                    }
                }
            }

            return dic;
        }

        private KoobooSetting GetSetting()
        {
            var methodInfo = ApiModel.GetMethodInfoByApi();
            var setting = methodInfo.GetCustomAttributes(typeof(KoobooSetting), true);
            return setting.ToList().Select(s => s as KoobooSetting).FirstOrDefault();
        }

        public string GetMethodBody()
        {
            var rules = GetRules();

            StringBuilder sb = new StringBuilder();
            //sb.Append($"{MethodName}:function(){{");
            sb.AppendLine().Append("var self=this;");
            #region valid method
            sb.AppendLine().Append("function isValid(){");

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
            sb.AppendLine().Append(triggerValid.ToString());
            sb.AppendLine().AppendFormat("return {0}", validCondition.ToString());

            sb.AppendLine().Append("}");
            sb.AppendLine().Append("if(!isValid()){return ;}");
            #endregion

            sb.AppendLine().AppendFormat("{0}({{",ApiModel.API);
            var datalist = GetDataList();
            var i = 0;
            foreach(var item in datalist)
            {
                if (i > 0)
                {
                    sb.Append(",");
                }
                sb.AppendLine().AppendFormat("{0}:self.$data.{0}",item.Key);
                i++;
            }
            sb.AppendLine().Append($"}}).then(function(res){{ self.{MethodCallBack}(res);}})");

            //sb.Append("}");

            return sb.ToString();
        }

        
    }
}
