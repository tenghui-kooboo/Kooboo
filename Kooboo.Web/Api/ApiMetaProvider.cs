using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Model.Render;
using Kooboo.Model.Render.ApiMeta;
using Kooboo.Model;
using Kooboo.Api;
using Kooboo.Model.ValidationRules;
using System.Reflection;
using System.Linq;

namespace Kooboo.Web.Api
{
    public class ApiMetaProvider : IApiMetaProvider
    {
        public ApiInfo GetMeta(string url)
        {
            var info = new ApiInfo();
            if (string.IsNullOrWhiteSpace(url))
            {
                return null;
            }
            var sep = "/\\_".ToCharArray();
            var parts = url.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
            {
                var method = CmsApiHelper.GetApiMethod(parts[0], parts[1]);
                info.Result = GetResult(method.ReturnType);
                info.Parameters = GetParameters(method.Parameters);
                info.Model = GetModel(method.Parameters);
            }

            return info;
        }

        private ModelInfo GetResult(Type type)
        {
            return GetModelInfo(type);
        }
        private ModelInfo GetModelInfo(Type type)
        {
            var properties = new List<Model.Render.ApiMeta.PropertyInfo>();
            var props = type.GetProperties();

            if (props != null)
            {
                foreach (var prop in props)
                {
                    var rules = new List<ValidationRule>();
                    var attributes = prop.GetCustomAttributes(typeof(ValidationRule));
                    if (attributes != null && attributes.Count() > 0)
                    {
                        rules = attributes.ToList().Select(r => r as ValidationRule).ToList();

                    }
                    properties.Add(new Model.Render.ApiMeta.PropertyInfo()
                    {
                        Name = prop.Name,
                        Type = prop.PropertyType,
                        Rules = rules
                    });

                }
            }

            var modelInfo = new ModelInfo()
            {
                Type=type,
                Properties= properties
            };

            return modelInfo;
        }

        private List<Model.Render.ApiMeta.PropertyInfo> GetParameters(List<Parameter> parameters)
        {
            var list = new List<Model.Render.ApiMeta.PropertyInfo>();
            foreach(var para in parameters)
            {
                list.Add(new Model.Render.ApiMeta.PropertyInfo()
                {
                   Name=para.Name,
                   Type=para.ClrType
                });
            }
            return list;
        }

        private ModelInfo GetModel(List<Parameter> parameters)
        {
            var model = parameters.Find(p => typeof(IKoobooModel).IsAssignableFrom(p.ClrType));
            var modelInfo = new ModelInfo();
            if (model != null)
            {
                modelInfo = GetModelInfo(model.ClrType);
            }
            return modelInfo;
        }
    }
}
