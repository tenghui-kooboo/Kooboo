using System;
using System.Collections.Generic;
using System.Linq;
using Kooboo.Model.ValidateRules;
using Kooboo.Model.Setting;
using System.Reflection;
using Kooboo.Api;

namespace Kooboo.Model.Helper
{
    public class RuleHelper
    {

        //api:Kooboo.User.login/Kooboo.User.post.login
        public static List<Rule> GetRuleByApi(string api, IApiProvider apiProvider)
        {
            var methodInfo =ModelApiHelper.GetMethodInfoByApi(api,apiProvider);

            var customRules = methodInfo.GetCustomAttributes(typeof(Rule), true);

            var rules = customRules.ToList().Select(r => r as Rule).ToList();

            return rules;

        }
        public static List<VueField> GetVueFields(Type type)
        {
            var dataList = new List<VueField>();
            var props = type.GetProperties();
            var attrPropers = typeof(Attribute).GetProperties();
            if (props != null)
            {
                foreach (var prop in props)
                {
                    var isAttrProp = attrPropers.ToList().Exists(a => a.Name == prop.Name);
                    if (isAttrProp) continue;

                    var field = new VueField();
                    field.Name = prop.Name;

                    if (prop.PropertyType.IsValueType)
                    {
                        field.Value= Activator.CreateInstance(type) as string;
                    }
                    field.ValidateRules = new List<Rule>();
                    var rules = prop.GetCustomAttributes(typeof(Rule));
                    if (rules != null && rules.Count() > 0)
                    {
                        field.ValidateRules = rules.ToList().Select(r => r as Rule).ToList();
                    }

                    dataList.Add(field);
                }
            }

            return dataList;

        }
       
    }
}
