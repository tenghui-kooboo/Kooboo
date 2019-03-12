using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Kooboo.Data.Context;
using Kooboo.Data.Language;
using Kooboo.Api;
using Kooboo.Model.Attributes;
using Kooboo.Model.Components;

namespace Kooboo.Model.Helper
{
    public class ModelHelper
    {
        public static string GetApi(string modelName)
        {
            if (!string.IsNullOrEmpty(modelName))
            {
                var type = ModelCache.Instance.Get(modelName);
                if (type != null)
                {
                    return GetApi(type);
                }
            }
            return string.Empty;
        }
        public static List<VueField> GetVueFields(string modelName, RenderContext context)
        {
            if (!string.IsNullOrEmpty(modelName))
            {
                var type =ModelCache.Instance.Get(modelName);
                if (type != null)
                {
                    return GetVueFields(type, context);
                }
            }
            return new List<VueField>();
        }
        public static List<VueField> GetVueFields(Type type, RenderContext context)
        {
            var components = ComponentManager.Instance.GetComponents(type);

            var list = new List<VueField>();
            foreach (var component in components)
            {
                list.Add(component.GetField());
            }

            var modelName = GetModelName(type);
            var modelField = new VueField()
            {
                Name = "modelName",
                Value = modelName
            };
            list.Add(modelField);

            var title = GetTitle(type);
            var titleField = new VueField()
            {
                Name = "title",
                Value = title
            };
            list.Add(titleField);
            return list;
        }

        private static string GetApi(Type type)
        {
            var api = string.Empty;
            var attr = type.GetCustomAttribute(typeof(ApiAttribute)) as ApiAttribute;
            if (attr != null)
            {
                api = attr.API;
            }
            return api;
        }

        private static string GetModelName(Type type)
        {
            var modelName = type.Name;
            var modelAttr = type.GetCustomAttribute(typeof(ModelNameAttribute)) as ModelNameAttribute;
            if (modelAttr != null)
            {
                modelName = modelAttr.ModelName;
            }
            return modelName;
        }

        private static string GetTitle(Type type)
        {
            var title = string.Empty;
            var attr = type.GetCustomAttribute(typeof(TitleAttribute)) as TitleAttribute;
            if (attr != null)
            {
                title = attr.Title;
            }
            return title;
        }

        /// <summary>
        /// get kooboosetting from api
        /// </summary>
        /// <param name="api"></param>
        /// <param name="apiProvider"></param>
        /// <returns></returns>
        public static KoobooSetting GetSetting(string api, IApiProvider apiProvider)
        {
            var methodInfo =ModelApiHelper.GetMethodInfoByApi(api, apiProvider);
            var setting = methodInfo.GetCustomAttributes(typeof(KoobooSetting), true);
            return setting.ToList().Select(s => s as KoobooSetting).FirstOrDefault();
        }
    }

    public class ComponentModel
    {
        public Type Type { get; set; }

        public List<Dictionary<string, List<Attribute>>> Attributes { get; set; }
    }
}
