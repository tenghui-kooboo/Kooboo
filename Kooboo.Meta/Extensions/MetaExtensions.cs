using Kooboo.Meta.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta
{
    public static class MetaExtensions
    {
        public static T LoadData<T>(this T view, string url) where T : KbView
        {
            return view.AddHook(KbView.Hook.Load(view.Id), $@"k.me.loadData(`{url}`)");
        }
    }
}
