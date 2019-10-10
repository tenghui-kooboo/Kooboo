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
            KbButton copyBtn = null;
            KbButton deleteBtn = null;
            KbTable pageTable = null;

            meta.LoadData("/_api/Page/all?SiteId=${k.query.SiteId}");

            meta.AddKbNavbar(navbar =>
            {
                navbar.AddButton(btn =>
                {
                    btn.Text = "新建页面";
                    btn.Color = "green";
                    btn.Redirect("/_Admin/Page/EditPage?SiteId=${k.query.SiteId}");
                });

                navbar.AddButton(btn =>
                {
                    btn.Text = "新建富文本页面";
                    btn.Color = "green";
                    btn.Redirect("/_Admin/Page/EditRichText?SiteId=${k.query.SiteId}");
                });

                navbar.AddDropdown(dropdown =>
                {
                    dropdown.Text = "使用布局新建页面";
                    dropdown.Color = "green";
                    dropdown.SetData(meta.Id, "k.data.layouts");
                    dropdown.SetItemTemplate(item =>
                    {
                        item.SetText("k.data.name");
                        item.Redirect("/_Admin/Page/EditLayout?SiteId=${k.query.SiteId}&layoutId=${k.self.data.id}");
                    });
                });

                copyBtn = navbar.AddButton(btn =>
                {
                    btn.Text = "复制";
                    btn.Color = "green";
                    btn.Visible = false;
                });

                deleteBtn = navbar.AddButton(btn =>
                {
                    btn.Text = "删除";
                    btn.Color = "red";
                    btn.Visible = false;
                });

                navbar.AddButton(btn =>
                {
                    btn.PullRight();
                    btn.Icon = "fa fa-gear";
                });
            });

            pageTable = meta.AddTable(table =>
            {
                table.ShowCheck = true;
                table.SetData(meta.Id, "k.data.pages");
                table.SetRowTemplate(row =>
                {
                    row.AddCell(cell =>
                    {
                        cell.Text = "列1";
                        cell.AddButton(btn =>
                        {
                            btn.Text = "zhansan";
                            btn.Color = "red";
                        });
                    });

                    var refCell = row.AddCell(cell =>
                    {
                        cell.Text = "列2";
                        cell.AddHook("dataChange", row.Id, "k.self.items=k.toList(k.data.relations)");
                        cell.SetItemTemplate(temp =>
                        {
                            var btn = new KbButton();
                            btn.Color = "red";
                            btn.AddHook("dataChange", temp.Id, "k.self.text=k.data.key");
                            temp.View = btn;
                        });
                    });

                });
            });

            copyBtn.AddHook("selected_rows_change", pageTable.Id, "k.self.visible=k.selectedRows.length==1");
            deleteBtn.AddHook("selected_rows_change", pageTable.Id, "k.self.visible=k.selectedRows.length>0");
        }
    }
}
