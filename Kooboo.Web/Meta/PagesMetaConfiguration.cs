using Kooboo.Meta;
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
            meta.AddView(new KbButton
            {
                Text = "按钮"
            });
        }
    }
}
