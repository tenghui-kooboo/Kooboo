using Kooboo.Meta;
using Kooboo.Meta.Models;
using Kooboo.Meta.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Web.Meta
{
    class PagesMetaConfiguration : IMetaConfiguration
    {
        public void Configure(IMeta meta)
        {
            var btn = meta.AddView(new KbButton
            {
                Text = "按钮"
            });

            btn.Hooks.Add(new Hook
            {
                Name=$"click_{btn.Id}",
                Action=$"k.self.style.color='red'"
            });
        }
    }
}
