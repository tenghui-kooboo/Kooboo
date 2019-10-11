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
                    btn.AddClass("green");
                    btn.Redirect("/_Admin/Page/EditPage?SiteId=${k.query.SiteId}");
                });

                navbar.AddButton(btn =>
                {
                    btn.Text = "新建富文本页面";
                    btn.AddClass("green");
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
                    btn.AddClass("green");
                    btn.Visible = false;
                });

                deleteBtn = navbar.AddButton(btn =>
                {
                    btn.Text = "删除";
                    btn.AddClass("red");
                    btn.Visible = false;
                });

                navbar.AddButton(btn =>
                {
                    btn.PullRight();
                    btn.AddClass("btn-default");
                    btn.Icon = new KbIcon
                    {
                        IconName = "fa fa-gear"
                    };
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
                        cell.Width = "20px";
                        cell.AddIcon(icon =>
                        {
                            icon.IconName = "fa fa-home fa-lg";
                            icon.AddHook("dataChange", row.Id, "k.self.visible=k.data.startPage");
                        });
                    });

                    row.AddCell(cell =>
                    {
                        cell.Text = "名称";
                        cell.AddText(text =>
                        {
                            text.AddHook("dataChange", row.Id, "k.self.text=k.data.name");
                        });
                    });

                    row.AddCell(cell =>
                    {
                        cell.Text = "被链接";
                        cell.AddBadge(badge =>
                        {
                            badge.AddClass("badge-primary");
                            badge.AddHook("dataChange", row.Id, "k.self.text=k.data.linked");
                        });
                    });

                    row.AddCell(cell =>
                    {
                        cell.Text = "在线情况";
                        cell.AddLabel(label =>
                        {
                            label.AddClass("label-sm label-success");
                            label.AddHook("dataChange", row.Id, "k.self.text=k.data.online");
                        });
                    });

                    var refCell = row.AddCell(cell =>
                    {
                        cell.Text = "引用";
                        cell.AddHook("dataChange", row.Id, "k.self.items=k.toList(k.data.relations)");
                        cell.SetItemTemplate(temp =>
                        {
                            var label = new KbLabel();
                            label.AddClass("label-sm label-success");
                            label.AddHook("dataChange", temp.Id, "k.self.text=`${k.data.value} ${k.data.key}`");
                            temp.View = label;
                        });
                    });

                    row.AddCell(cell =>
                    {
                        cell.Text = "修改时间";
                        cell.AddText(text =>
                        {
                            text.AddHook("dataChange", row.Id, "k.self.text=k.data.lastModified");
                        });
                    });

                    row.AddCell(cell =>
                    {
                        cell.Text = "预览";
                        cell.AddButton(btn =>
                        {
                            btn.AddClass("btn-link btn-sm");
                            btn.AddHook("dataChange", row.Id, "k.self.text=k.data.path");
                            btn.AddHook("click", btn.Id, $"window.open(k.pool.{row.Id}.data.previewUrl)");
                        });
                    });

                    row.AddCell(cell =>
                    {
                        cell.Width = "180px";
                        cell.AddButton(btn =>
                        {
                            btn.Text = "设置";
                            btn.AddClass("blue btn-sm");
                            btn.AddHook("click", btn.Id, $@"
var rowData=k.pool.{row.Id}.data;
if(rowData.layoutId !='00000000-0000-0000-0000-000000000000')location.href='/_admin/Page/EditLayout?siteId='+k.query.SiteId+'&id='+rowData.id+'&layoutId='+rowData.layoutId;
else if(rowData.type='Normal') location.href='/_admin/Page/EditPage?siteId='+k.query.SiteId+'&id='+rowData.id;
else location.href ='/_admin/Page/EditRichText?siteId='+k.query.SiteId+'&id='+rowData.id;
");
                        });

                        cell.AddButton(btn =>
                        {
                            btn.Text = "在线编辑";
                            btn.AddClass("btn-primary btn-sm");
                            btn.AddHook("click", btn.Id, $"window.open(k.pool.{row.Id}.data.inlineUrl)");
                        });
                    });

                });
            });

            copyBtn.AddHook("selected_rows_change", pageTable.Id, "k.self.visible=k.selectedRows.length==1");
            deleteBtn.AddHook("selected_rows_change", pageTable.Id, "k.self.visible=k.selectedRows.length>0");
        }
    }
}
