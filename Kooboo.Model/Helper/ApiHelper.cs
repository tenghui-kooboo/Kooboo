using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Kooboo.Api;

namespace Kooboo.Model.Helper
{
    public class ModelApiHelper
    {
        public static MethodInfo GetMethodInfoByApi(string api, IApiProvider apiProvider)
        {
            var method = GetMethod(api);

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
