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
        public void Configure(Kooboo.Meta.Views.Meta meta)
        {
            meta.AddKbNavbar(navbar =>
            {
                navbar.AddButton(newPageBtn =>
                {
                    newPageBtn.Text = "新建页面";
                    newPageBtn.AddHook(hook =>
                    {
                        hook.Name = $"click_{newPageBtn.Id}";
                        hook.Action = $"k.self.style.color='red'";
                    });
                });
            });
        }
    }
}
