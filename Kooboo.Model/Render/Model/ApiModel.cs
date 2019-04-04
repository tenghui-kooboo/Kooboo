using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Kooboo.Api;

namespace Kooboo.Model.Render.Model
{
    public class ApiModel
    {
        public string API { get; set; }

        public string Class { get; set; }

        public string Method { get; set; }

        public ApiRequestMethod RequestMethod { get; set; }

        private IApiProvider _provider;

        public ApiModel(string api,IApiProvider provider)
        {
            _provider = provider;
            API = api;
            Init(api);
        }
        private void Init(string api)
        {
            var parts = api.Split('.');

            var method = new ApiMethod();
            if (parts.Length >= 3)
            {
                var partOne = parts[0];
                if (partOne.Equals("kooboo", StringComparison.OrdinalIgnoreCase))
                {
                    this.Class = parts[1];
                    this.Method = parts[2];
                    
                    if (parts.Length > 3)
                    {
                        if (parts[3].ToLower() == "post")
                        {
                            this.RequestMethod = ApiRequestMethod.POST;
                        }

                    }
                }
            }
        }
        public MethodInfo GetMethodInfoByApi()
        {
            var apiobject = _provider.Get(this.Class);
            if (apiobject == null)
            {
                Console.WriteLine(string.Format("Object type {0} Not Found", this.Class));
                return null;
            }
            var methodInfo = GetMethodInfo(apiobject.GetType(), this.Method);
            if (methodInfo == null)
            {
                Console.WriteLine(string.Format("Api method {0} Not Found", this.Method));
                return null;
            }
            return methodInfo;
        }

        private MethodInfo GetMethodInfo(Type type, string methodname)
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

    public enum ApiRequestMethod
    {
        GET,
        POST
    }
}
