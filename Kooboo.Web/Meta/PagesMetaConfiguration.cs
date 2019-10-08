using Kooboo.Meta;
using Kooboo.Meta.Models;
using Kooboo.Meta.Views;
using Kooboo.Meta.Views.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Web.Meta
{
    class PagesMetaConfiguration : IMetaConfiguration
    {
        public void Configure(KbMeta meta)
        {
            meta.LoadData("/_api/Page/all?SiteId=${k.query.SiteId}");

            meta.AddKbNavbar(navbar =>
            {

                navbar.AddButton(btn =>
                {
                    btn.Text = "新建页面";
                    btn.SetGreen();
                    btn.Redirect("/_Admin/Page/EditPage?SiteId=${k.query.SiteId}");
                });

                navbar.AddButton(btn =>
                {
                    btn.Text = "新建富文本页面";
                    btn.SetGreen();
                    btn.Redirect("/_Admin/Page/EditRichText?SiteId=${k.query.SiteId}");
                });

                navbar.AddDropdown(dropdown =>
                {
                    dropdown.Text = "使用布局新建页面";
                    dropdown.SetGreen();
                    dropdown.AddItemTemplate(item =>
                    {
                        item.AddHook("dataChange", item.Id, "k.self.text=k.data.name");
                        item.Redirect("/_Admin/Page/EditLayout?SiteId=${k.query.SiteId}&layoutId=${k.self.data.id}");
                    });
                    dropdown.AddHook("DataLoad", meta.Id, $"k.self.dataList=k.data.layouts;");
                });

                var copyBtn = navbar.AddButton(btn =>
                 {
                     btn.Text = "复制";
                     btn.SetGreen();
                     btn.AddHook("load", btn.Id, "k.self.visible=false");
                     btn.AddHook("click", btn.Id, "k.self.visible=false");
                 });

                navbar.AddButton(btn =>
                {
                    btn.Text = "删除";
                    btn.SetRed();
                    btn.AddHook("click", btn.Id, $"k.pool['{copyBtn.Id}'].visible=true");
                    btn.AddHook("click", btn.Id, $"k.pool['{copyBtn.Id}'].enable=false");
                });

                navbar.AddButton(btn =>
                {
                    btn.PullRight();
                    btn.SetIcon("fa fa-gear");
                });
            });


        }
    }
}
