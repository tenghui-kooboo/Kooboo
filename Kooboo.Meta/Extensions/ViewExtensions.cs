using Kooboo.Meta.Models;
using Kooboo.Meta.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta
{
    public static class ViewExtensions
    {
        public static T AddHook<T>(this T view, Action<KbHook> action) where T : KbView
        {
            var hook = new KbHook();
            action(hook);
            view.Hooks.Add(hook);
            return view;
        }

        public static T AddHook<T>(this T view, string name, string execute) where T : KbView
        {

            view.AddHook(hook =>
            {
                hook.Name = name;
                hook.Execute = execute;
            });

            return view;
        }
    }
}
