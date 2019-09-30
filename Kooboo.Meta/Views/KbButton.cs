using Kooboo.Meta.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views
{
    public class KbButton : KbView
    {
        public new class Hook : KbView.Hook
        {
            public static string Click(string id) => $"{nameof(Click)}_{id}";
        }

        public override string Name => nameof(KbButton);

        public string Text { get; set; }

        public KbButton NewWindow(string url)
        {
            return this.AddHook(Hook.Click(Id), $"window.open(`{url}`)");
        }

        public KbButton Redirect(string url)
        {
            return this.AddHook(Hook.Click(Id), $"location.href=`{url}`");
        }

        public KbButton Execute(string code)
        {
            return this.AddHook(Hook.Click(Id), code);
        }

    }
}
