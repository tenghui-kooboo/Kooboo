using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Model.Meta.Tab;
using Kooboo.Data.Interface;
using Kooboo.Model.Meta;
using Kooboo.Sites.Models;
using Kooboo.Web.Api.Implementation;
using Kooboo.Model.Meta.Table;
using Kooboo.Web.ViewModel;
using Kooboo.Model.Meta.Popup;

namespace Kooboo.Web.Meta
{
    public class FormsMeta : ITabMetaConfigure<Form>
    {
        public void Configure(TabMeta meta)
        {
            meta.Tabs.Add(new Tab()
            {
                Text = new Localizable("External"),
                DataApi = UrlHelper.ApiUrl<FormApi>(nameof(FormApi.External)),
                View = CreateTableMeta()
            });

            meta.Tabs.Add(new Tab()
            {
                Text = new Localizable("Embedded"),
                DataApi = UrlHelper.ApiUrl<FormApi>(nameof(FormApi.Embedded)),
                View = CreateTableMeta()
            });

        }

        private TableMeta CreateTableMeta()
        {
            var tableMeta = new TableMeta();

            tableMeta.Menu.Add(new ButtonMenu
            {
                Text = new Localizable("Create a form"),
                Class = "green",
                Action = new ActionMeta
                {
                    Type = ActionType.Redirect,
                    Url = "/_Admin/Development/Form"
                }
            });

            tableMeta.Menu.Add(new ButtonMenu
            {
                Text = new Localizable("Create a Kform"),
                Class = "green",
                Action = new ActionMeta
                {
                    Type = ActionType.Redirect,
                    Url = "/_Admin/Development/KooForm"
                }
            });
            tableMeta.Menu.Add(new ButtonMenu
            {
                Text = new Localizable("Delete"),
                Class = "red",
                Action = new ActionMeta
                {
                    Type = ActionType.Post,
                    Confirm = "confirm.deleteItems",
                    Url = UrlHelper.ApiUrl<FormApi>(nameof(PageApi.Deletes))
                },
                Visible = Comparison.EqualOrGreaterThan(1)
            });

            tableMeta.Builder<FormListItemViewModel>()
                .Column<TextCell>(o => o.Name, new Localizable("Name"), null)
                .Column<BadgeCell>(o => o.ValueCount, new Localizable("Data"), null)
                .Column<ArrayCell>(o => o.References, new Localizable("Used by"), cell =>
                {
                    cell.Text = Localizable.Raw("{0} {key}");
                    cell.Action = ActionMeta.Popup(UrlHelper.PopupMetaUrl<RelationPopup>());
                })
                .Column<DateCell>(o => o.LastModified, new Localizable("Last modified"), null)
                .Column<ButtonCell>("edit", Localizable.Raw(""), cell =>
                {
                    cell.Class = "btn-primary hidden-xs";
                    cell.Text = new Localizable("edit");
                    cell.Action = new ActionMeta
                    {
                        Type = ActionType.Redirect,
                        Url = "/_Admin/Development/Form?id={id}"
                    };
                })
                .Column<ButtonCell>("settting", Localizable.Raw(""), cell =>
                {
                    cell.Class = "btn-primary hidden-xs";
                    cell.Text = new Localizable("settting");
                    cell.Action = new ActionMeta
                    {
                        Type = ActionType.Popup,
                        Meta= ActionMeta.EmbeddedPopup(CreateSettingPopup())
                    };
                })
                ;

            return tableMeta;
        }

        private PopupMeta CreateSettingPopup()
        {
            PopupMeta popupMeta = new PopupMeta();
            return popupMeta;
        }
    }

    //public class Forms : ISiteObject
    //{
    //    public byte ConstType { get; set; }
    //    public DateTime CreationDate { get; set; }
    //    public DateTime LastModified { get; set; }
    //    public Guid Id { get; set; }
    //    public string Name { get; set; }
    //}
}
