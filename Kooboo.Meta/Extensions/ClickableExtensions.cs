using Kooboo.Meta.Models;
using Kooboo.Meta.Views.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta
{
    public static class ClickableExtensions
    {
        public static T NewWindow<T>(this T view, JsString url) where T : KbClickable
        {
            return view.AddHook("click", view.Id, $"window.open(`{url}`)");
        }

        public static T Redirect<T>(this T view, JsString url) where T : KbClickable
        {
            return view.AddHook("click", view.Id, $"location.href=`{url}`");
        }

        public static T Execute<T>(this T view, JsCode code) where T : KbClickable
        {
            return view.AddHook("click", view.Id, code);
        }
    }
}
