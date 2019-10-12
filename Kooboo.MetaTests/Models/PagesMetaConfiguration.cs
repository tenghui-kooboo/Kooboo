using Kooboo.Meta.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Tests.Infrastructure
{
    class PagesMetaConfiguration : IMetaConfiguration
    {
        public void Configure(KbMeta meta)
        {
            meta.AddView(new KbButton
            {
                Text = "按钮"
            });
        }
    }
}
