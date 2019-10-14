using Kooboo.Data.Language;
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
                    btn.Text = LanguageProvider.GetValue("New page");
                    btn.AddClass("green");
                    btn.Redirect("/_Admin/Page/EditPage?SiteId=${k.query.SiteId}");
                });

                navbar.AddButton(btn =>
                {
                    btn.Text = LanguageProvider.GetValue("New rich text page");
                    btn.AddClass("green");
                    btn.Redirect("/_Admin/Page/EditRichText?SiteId=${k.query.SiteId}");
                });

                navbar.AddDropdown(dropdown =>
                {
                    dropdown.Text = LanguageProvider.GetValue("New layout page");
                    dropdown.Color = "green";
                    dropdown.SetData(meta.Id, "k.data.layouts");
                    dropdown.SetItemTemplate(item =>
                    {
                        item.SetText("k.data.name");
                        item.Redirect("/_Admin/Page/EditLayout?SiteId=${k.query.SiteId}&layoutId=${k.self.data.id}");
                    });
                });

                navbar.AddButton(btn =>
                 {
                     btn.Text = LanguageProvider.GetValue("Import");
                     btn.AddClass("green");
                     btn.ShowPopup("pages_import_popup");
                 });

                navbar.AddButton(btn =>
                {
                    btn.Text = LanguageProvider.GetValue("Copy");
                    btn.AddClass("green");
                    btn.Visible = false;
                    btn.AddHook("selectedRowsChange", "pages_table", "k.self.visible=k.selectedRows.length==1");
                });

                navbar.AddButton(btn =>
                {
                    btn.Text = LanguageProvider.GetValue("Delete");
                    btn.AddClass("red");
                    btn.Visible = false;
                    btn.AddHook("selectedRowsChange", "pages_table", "k.self.visible=k.selectedRows.length>0");
                });

                navbar.AddButton(btn =>
                {
                    btn.PullRight();
                    btn.AddClass("btn-default");
                    btn.Icon = new KbIcon
                    {
                        IconName = "fa fa-gear"
                    };
                    btn.ShowPopup("pages_router_settings_popup");
                });
            });

            meta.AddTable(table =>
            {
                table.Id = "pages_table";
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
                        cell.Text = LanguageProvider.GetValue("Name");
                        cell.AddText(text =>
                        {
                            text.AddHook("dataChange", row.Id, "k.self.text=k.data.name");
                        });
                    });

                    row.AddCell(cell =>
                    {
                        cell.Text = LanguageProvider.GetValue("Linked");
                        cell.AddBadge(badge =>
                        {
                            badge.AddClass("badge-primary");
                            badge.AddHook("dataChange", row.Id, "k.self.text=k.data.linked");
                        });
                    });

                    row.AddCell(cell =>
                    {
                        cell.Text = LanguageProvider.GetValue("Online");
                        cell.AddLabel(label =>
                        {
                            label.AddClass("label-sm label-success");
                            label.AddHook("dataChange", row.Id, "k.self.text=k.data.online?k.text.online.yes:k.text.online.no");
                        });
                    });

                    var refCell = row.AddCell(cell =>
                    {
                        cell.Text = LanguageProvider.GetValue("References");
                        cell.AddHook("dataChange", row.Id, "k.self.items=k.toList(k.data.relations)");
                        cell.SetItemTemplate(temp =>
                        {
                            var label = new KbLabel();
                            label.AddClass("label-sm label-success");
                            label.AddHook("dataChange", temp.Id, "k.self.text=`${k.data.value} ${k.text.component.table[k.data.key]}`");
                            label.AddHook("click", label.Id, $@"
let rowData= k.pool.{row.Id}.data;
k.pool.pages_relation_popup.$loadData(`/_api/Relation/ShowBy?SiteId=${{k.query.SiteId}}&id=${{rowData.id}}&by=${{k.pool.{temp.Id}.data.key}}&type=Page`);
k.pool.pages_relation_popup.visible=true;
");
                            temp.View = label;
                        });
                    });

                    row.AddCell(cell =>
                    {
                        cell.Text = LanguageProvider.GetValue("Last modified");
                        cell.AddText(text =>
                        {
                            text.AddHook("dataChange", row.Id, "k.self.text=k.datetimeFormat(k.data.lastModified)");
                        });
                    });

                    row.AddCell(cell =>
                    {
                        cell.Text = LanguageProvider.GetValue("Preview");
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
                            btn.Text = LanguageProvider.GetValue("Settings");
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
                            btn.Text = LanguageProvider.GetValue("Inline edit");
                            btn.AddClass("btn-primary btn-sm");
                            btn.AddHook("click", btn.Id, $"window.open(k.pool.{row.Id}.data.inlineUrl)");
                        });
                    });

                });
            });

            meta.AddPopup(popup =>
             {
                 popup.Id = "pages_router_settings_popup";
                 popup.Title = "路由设置";

                 popup.AddForm(form =>
                 {
                     form.Fields.Add(new Field
                     {
                         Label = "name"
                     });
                 });

                 popup.AddFooterButton(btn =>
                 {
                     btn.Text = "关闭";
                     btn.AddClass("btn-default");
                     btn.ClosePopup(popup.Id);
                 });

                 popup.AddFooterButton(btn =>
                 {
                     btn.Text = "开始";
                     btn.AddClass("green");
                     btn.ClosePopup(popup.Id);
                 });
             });

            meta.AddPopup(popup =>
            {
                popup.Id = "pages_import_popup";
                popup.Title = LanguageProvider.GetValue("Import");
            });

            meta.AddPopup(popup =>
            {
                popup.Id = "pages_relation_popup";
                popup.AddTable(table =>
                {
                    table.Id = "pages_relation_popup_table";
                    table.SetData("pages_relation_popup", "k.data");
                    table.SetRowTemplate(row =>
                    {
                        row.AddCell(cell =>
                        {
                            cell.Text = "名称";
                            cell.AddButton(btn =>
                            {
                                btn.AddClass("btn-link");
                                btn.AddHook("dataChange", row.Id, $"k.self.text=k.data.name");
                                btn.NewWindow($"${{k.pool.{row.Id}.data['url']}}");
                            });
                        });

                        row.AddCell(cell =>
                        {
                            cell.Text = "备注";
                            cell.AddText(text =>
                            {
                                text.AddHook("dataChange", row.Id, $"k.self.text=k.data.remark||'-'");
                            });
                        });
                    });

                });
            });
        }
    }
}
