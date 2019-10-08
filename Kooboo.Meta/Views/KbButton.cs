using Kooboo.Meta.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views
{
    public class KbButton : KbView
    {
        public override string Name => nameof(KbButton);

        public string Text { get; set; }

        public KbButton NewWindow(string url)
        {
            return this.AddHook("click", Id, $"window.open(`{url}`)");
        }

        public KbButton Redirect(string url)
        {
            return this.AddHook("click", Id, $"location.href=`{url}`");
        }

        public KbButton Execute(string code)
        {
            return this.AddHook("click", Id, code);
        }

    }
}
