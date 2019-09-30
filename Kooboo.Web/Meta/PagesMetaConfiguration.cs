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
        public void Configure(KbMeta meta)
        {
            meta.AddKbNavbar(navbar =>
            {
                navbar.AddButton(newPageBtn =>
                {
                    newPageBtn.Text = "新建页面";
                    newPageBtn.NewWindow("http://www.baidu.com");
                });

                navbar.AddButton(newPageBtn =>
                {
                    newPageBtn.Text = "新建页面(重定向)";
                    newPageBtn.Redirect("http://www.baidu.com");
                });

                navbar.AddButton(newRichPageBtn =>
                {
                    newRichPageBtn.Text = "新建富文本页面";
                    newRichPageBtn.AddHook(hook =>
                    {
                        hook.Name = KbButton.Hook.click.ToName(newRichPageBtn.Id);
                        hook.Execute = $"k.self.style.color='red'";
                    });
                });
            });
        }
    }
}
