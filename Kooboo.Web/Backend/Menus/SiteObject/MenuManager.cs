using Kooboo.Data.Interface;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

using Kooboo.Model.Meta;
using Kooboo.Model.Meta.Table;

namespace Kooboo.Web.Menus.ObjectMenu
{
    public static class MenuManager
    {
        public static Dictionary<string, SiteObjectMenu> GetSiteMenuApiMethodByType(Type SiteObjectType)
        {
            var result = new Dictionary<string, SiteObjectMenu>();

            var provider = GetApiProvider();
            var name = SiteObjectType.Name;

            if (provider.List.ContainsKey(name))
            {
                var allmethods = Lib.Reflection.TypeHelper.GetPublicMethods(SiteObjectType);

                foreach (var item in allmethods)
                {
                    var attr = GetMenuAttribute(item);
                    if (attr != null)
                    {
                        result.Add(item.Name, attr);
                    }
                }

                return result;
            }

            return null;
        }

        public static Kooboo.Api.IApiProvider GetApiProvider()
        {
            foreach (var item in Web.SystemStart.Middleware)
            {
                if (item is Kooboo.Api.ApiMiddleware)
                {
                    var apimiddle = item as Kooboo.Api.ApiMiddleware;
                    return apimiddle.ApiProvider;
                }
            }
            return null;
        }

        public static SiteObjectMenu GetMenuAttribute(MethodInfo method)
        {
            return method.GetCustomAttribute(typeof(SiteObjectMenu)) as SiteObjectMenu;
        }
    }

    public static class TableMetaBuilderExtensions
    {
        private static Type ViewModelGeneric = typeof(IViewModel<>);

        public static TableMetaBuilder<TModel> MergeApi<TModel, TEntity>(this TableMetaBuilder<TModel> builder)
            where TEntity : ISiteObject
        {
            return MergeApi<TModel>(builder, typeof(TEntity));
        }

        public static TableMetaBuilder<TModel> MergeApi<TModel>(this TableMetaBuilder<TModel> builder)
        {
            var viewModelType = typeof(TModel);
            var interfaceType = viewModelType.GetInterfaces().FirstOrDefault(o => o.IsGenericType && o.GetGenericTypeDefinition() == ViewModelGeneric);
            if (interfaceType == null)
                throw new InvalidOperationException("Can only guess Api for view model who implement IViewModel<T>");

            var entityType = interfaceType.GetGenericArguments()[0];
            return MergeApi(builder, entityType);
        }

        private static TableMetaBuilder<TModel> MergeApi<TModel>(TableMetaBuilder<TModel> builder, Type entityType)
        {
            var methods = MenuManager.GetSiteMenuApiMethodByType(entityType);
            foreach (var each in methods)
            {
                var methodName = each.Key;
                var attr = each.Value;
                var url = UrlHelper.ApiUrl(entityType, methodName);
                builder.Meta.Menu.Add(new ButtonMenu
                {
                    Name = methodName,
                    Text = new Localizable(attr.Text),
                    Align = attr.AlignRight ? MenuAlign.Right : MenuAlign.Left,
                    Visible = attr.Multiple ? Comparison.OnMultipleSelection : Comparison.OnSingleSelection,
                    Class = attr.Class,
                    Action = ActionMeta.Post(url)
                });
            }

            return builder;
        }
    }
}
