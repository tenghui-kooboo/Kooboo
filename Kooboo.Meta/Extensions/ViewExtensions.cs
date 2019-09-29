using Kooboo.Meta.Models;
using Kooboo.Meta.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta
{
    public static class ViewExtensions
    {
        public static Hook AddHook<T>(this T view, Action<Hook> action) where T : View
        {
            var hook = new Hook();
            action(hook);
            view.Hooks.Add(hook);
            return hook;
        }
    }
}
