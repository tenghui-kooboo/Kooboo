using Kooboo.Meta.Models;
using Kooboo.Meta.Views.Abstracts;
using System;

namespace Kooboo.Meta
{
    public static class ViewExtensions
    {
        public static T AddHook<T>(this T view, Action<KbHook> action) where T : KbView
        {
            view.Hooks.Add(new KbHook(action));
            return view;
        }

        public static T AddHook<T>(this T view, string name, JsCode execute) where T : KbView
        {
            view.Hooks.Add(new KbHook(name, execute));
            return view;
        }

        public static T AddHook<T>(this T view, string hookName, string id, JsCode execute) where T : KbView
        {
            return view.AddHook($"{hookName}_{id}", execute);
        }

        public static T LoadData<T>(this T view, JsString url) where T : KbView
        {
            return view.AddHook("load", view.Id, $"k.self.$loadData(`{url}`)");
        }
    }
}
