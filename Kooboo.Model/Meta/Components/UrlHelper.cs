using System;
using System.Collections.Generic;
using System.Linq;

using Kooboo.Data.Interface;
using Kooboo.Model.Meta.Popup;

namespace Kooboo.Model.Meta
{
    public class UrlHelper
    {
        public static string PopupMetaUrl<TModel>()
            where TModel : ISiteObject
        {
            return MetaUrl<TModel, PopupMeta>();
        }

        public static string MetaUrl<TModel, TMeta>()
            where TModel : ISiteObject
            where TMeta : IViewMeta
        {
            if (!MetaProvider.Instance.IsValidPair<TModel, TMeta>())
                throw new NotImplementedException($"PageMeta configure not found for model {typeof(TModel).FullName}");

            var metaName = typeof(TMeta).Name;
            metaName = metaName.Substring(metaName.Length - 4);
            return $"/meta/${metaName}?modelName={typeof(TModel).Name}";
        }

        public static string ApiUrl<TApi>(string method)
        {
            return ApiUrl(typeof(TApi), method);
        }

        public static string ApiUrl(Type apiType, string method)
        {
            var apiUrl = MetaProvider.ApiMetaProvider.GetModelName(apiType) + "/" + method;
            var meta = MetaProvider.ApiMetaProvider.GetMeta(apiUrl);

            return ApiUrl(apiUrl, meta.Parameters);
        }

        public static string ApiUrl(string url, IEnumerable<Api.PropertyInfo> parameters)
        {
            if (parameters == null || !parameters.Any())
                return url;

            var query = String.Join("&", parameters.Select(o => ToJsName(o.Name)).Select(o => $"{o}={{{o}}}"));
            return $"{url}?{query}";
        }

        public static string ToJsName(string name)
        {
            if (name.Length == 1)
                return name.ToLower();

            return Char.ToLower(name[0]) + name.Substring(1);
        }



    }
}
