using Kooboo.Meta.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views
{
    public class KbButton : KbView
    {
        public enum Hook
        {
            click
        }

        public override string Name => nameof(KbButton);

        public string Text { get; set; }

        public KbButton NewWindow(string url)
        {
            return this.AddHook(Hook.click.ToName(Id), $"window.open('{url}')");
        }

        public KbButton Redirect(string url)
        {
            return this.AddHook(Hook.click.ToName(Id), $"location.href='{url}'");
        }

        public KbButton Execute(string code)
        {
            return this.AddHook(Hook.click.ToName(Id), code);
        }

    }
}
