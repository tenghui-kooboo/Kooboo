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
            var methodInfo = GetMethodInfoByApi(api,apiProvider);

            var customRules = methodInfo.GetCustomAttributes(typeof(Rule), true);

            var rules = customRules.ToList().Select(r => r as Rule).ToList();

            return rules;

        }

        public static RuleSetting GetRuleSettingByApi(string api, IApiProvider apiProvider)
        {
            var methodInfo = GetMethodInfoByApi(api,apiProvider);
            var setting = methodInfo.GetCustomAttributes(typeof(RuleSetting), true);
            return setting.ToList().Select(s => s as RuleSetting).FirstOrDefault();
        }

        


        public static MethodInfo GetMethodInfoByApi(string api, IApiProvider apiProvider)
        {
            var method = GetMethod(api);
            var rules = new List<Rule>();

            var apiobject = apiProvider.Get(method.Class);
            if (apiobject == null)
            {
                Console.WriteLine(string.Format("Object type {0} Not Found", method.Class));
                return null;
            }
            var methodInfo = GetMethodInfo(apiobject.GetType(), method.Method);
            if (methodInfo == null)
            {
                Console.WriteLine(string.Format("Api method {0} Not Found", method.Class));
                return null;
            }
            return methodInfo;
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
        


        public static ApiMethod GetMethod(string api)
        {
            var parts = api.Split('.');

            var method = new ApiMethod();
            if (parts.Length >= 3)
            {
                var partOne = parts[0];
                if (partOne.Equals("kooboo", StringComparison.OrdinalIgnoreCase))
                {
                    method.Class = parts[1];
                    method.Method = parts[2];
                    method.Type = "GET";
                    if (parts.Length > 3)
                    {
                        var postMethods = new List<string>() { "get", "post" };

                        var type = "GET";
                        if (postMethods.Contains(method.Method.ToLower()))
                        {
                            type = method.Method.ToUpper();
                        }
                        method.Type = type;
                        method.Method = parts[3];
                    }
                }
            }
            return method;
        }

        private static MethodInfo GetMethodInfo(Type type, string methodname)
        {
            var method = type.GetMethod(methodname);
            if (method != null)
            {
                return method;
            }

            var lowername = methodname.ToLower();

            var allmethods = Kooboo.Lib.Reflection.TypeHelper.GetPublicMethods(type);
            foreach (var item in allmethods)
            {
                if (item.Name.ToLower() == lowername)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
