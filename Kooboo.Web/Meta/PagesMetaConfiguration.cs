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
            meta.LoadData("/_api/Page/all?SiteId=${k.query.SiteId}");

            meta.AddKbNavbar(navbar =>
            {
                //navbar.AddButton(btn =>
                //{
                //    btn.Text = "测试";
                //    btn.Execute("console.log(k)");
                //});

                navbar.AddButton(btn =>
                {
                    btn.Text = "新建页面";
                    btn.Redirect("/_Admin/Page/EditPage?SiteId=${k.query.SiteId}");
                });

                navbar.AddButton(btn =>
                {
                    btn.Text = "新建富文本页面";
                    btn.Redirect("/_Admin/Page/EditRichText?SiteId=${k.query.SiteId}");
                });

                navbar.AddDropdown(dropdown =>
                {
                    dropdown.Text = "使用布局新建页面";
                    dropdown.AddItemTemplate(item =>
                    {
                        item.Text = "name";
                        item.Redirect("/_Admin/Page/EditLayout?SiteId=${k.query.SiteId}&layoutId=${k.me.data.id}");
                    });
                    dropdown.AddHook(KbView.Hook.dataLoad.ToName(meta.Id), "k.me.dataList=k.data.layouts");
                });
            });
        }
    }
}
