using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Model.Render;
using Kooboo.Model.Meta.Api;
using ApiMeta = Kooboo.Model.Meta.Api;
using Kooboo.Model;
using Kooboo.Api;
using Kooboo.Model.Meta.Validation;
using System.Reflection;
using System.Linq;

namespace Kooboo.Web.Api
{
    public class ApiMetaProvider : IApiMetaProvider
    {
        public ApiInfo GetMeta(string url)
        {
            if (String.IsNullOrEmpty(url))
                ThrowInvalidUrlException();

            var sep = "/\\_".ToCharArray();
            var parts = url.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2)
                ThrowInvalidUrlException();

            var method = CmsApiHelper.GetApiMethod(parts[0], parts[1]);

            var info = new ApiInfo
            {
                Result = GetModelInfo(method.ReturnType)
            };
            
            foreach (var p in method.Parameters)
            {
                if (p.ClrType == typeof(ApiCall))
                    continue;

                if (IsComplexType(p.ClrType))
                {
                    info.Model = GetModelInfo(p.ClrType);
                }
                else
                {
                    info.Parameters.Add(new ApiMeta.PropertyInfo
                    {
                        Name = p.Name,
                        Type = p.ClrType
                    });
                }
            }

            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">Class inherit from iapi</param>
        /// <returns></returns>
        public string GetModelName(Type type)
        {
            var instance = Activator.CreateInstance(type) as IApi;
            if (instance != null)
            {
                return instance.ModelName;
            }
            return string.Empty;

        }
        private ModelInfo GetModelInfo(Type type)
        {
            return new ModelInfo()
            {
                Type = type,
                Properties = type.GetProperties().Select(p => new ApiMeta.PropertyInfo
                {
                    Name = p.Name,
                    Type = p.PropertyType,
                    Rules = p.GetCustomAttributes(typeof(ValidationRule)).Cast<ValidationRule>().ToList()
                }).ToList()
            };
        }

        private List<ApiMeta.PropertyInfo> GetParameters(List<Parameter> parameters)
        {
            var list = new List<ApiMeta.PropertyInfo>();
            foreach(var para in parameters)
            {
                list.Add(new ApiMeta.PropertyInfo()
                {
                   Name=para.Name,
                   Type=para.ClrType
                });
            }
            return list;
        }

        private ModelInfo GetModel(List<Parameter> parameters)
        {
            var model = parameters.Find(p => IsComplexType(p.ClrType));
            var modelInfo = new ModelInfo();
            if (model != null)
            {
                modelInfo = GetModelInfo(model.ClrType);
            }
            return modelInfo;
        }

        private bool IsComplexType(Type type)
        {
            if (type.IsValueType || type.IsArray || type == typeof(String))
                return false;

            if (type == typeof(ApiCall))
                return false;

            return true;
        }

        private void ThrowInvalidUrlException() => throw new ArgumentException("Invalid api url");
    }
}
