using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Web.Api.Implementation;
using Kooboo.Sites.Models;
using Kooboo.Model.Meta.Table;
using Kooboo.Model.Meta.Popup;
using Kooboo.Model.Meta.Form;
using Kooboo.Model.Meta;
using Kooboo.Web.ViewModel;
using Kooboo.Model.Meta.Validation;

namespace Kooboo.Web.Meta
{
    public class PageMeta : ITableMetaConfigure<Page>
    {
        public bool IsCreator => true;

        public void Configure(TableMeta meta)
        {
            meta.DataApi = UrlHelper.ApiUrl<PageApi>(nameof(PageApi.All));
            meta.ListName = "page";

            #region menu
            meta.Menu.Add(new ButtonMenu
            {
                Text = new Localizable("New page"),
                Class = "green",
                Action = new ActionMeta
                {
                    Type = ActionType.Redirect,
                    Url = "/_Admin/Page/EditPage"
                }
            });

            meta.Menu.Add(new ButtonMenu
            {
                Text = new Localizable("New rich text page"),
                Class = "green",
                Action = new ActionMeta
                {
                    Type = ActionType.Redirect,
                    Url = "/_Admin/Page/EditRichText"
                }
            });

            meta.Menu.Add(new DropDownMenu
            {
                Text = new Localizable("New layout page"),
                Class = "green",
                Action = new ActionMeta
                {
                    Type = ActionType.Redirect,
                    Url = "/_Admin/Page/EditLayout?layoutId={id}"
                },
                Options = SelectOptions.UseContext("layouts", "{name}")
            });

            meta.Menu.Add(new ButtonMenu
            {
                Text = new Localizable("Import"),
                Class = "green",
                Action = new ActionMeta
                {
                    Type = ActionType.Popup,

                }
            });

            meta.Menu.Add(new ButtonMenu
            {
                Text = new Localizable("Copy"),
                Class = "green",
                Action = ActionMeta.EmbeddedPopup(MetaHelper.CreateCopyPopupMeta()),
                Visible = Comparison.Equal(1)
            });

            meta.Menu.Add(new ButtonMenu
            {
                Text = new Localizable("Delete"),
                Class = "red",
                Action = new ActionMeta
                {
                    Type = ActionType.Post,
                    Confirm = "confirm.deleteItems",
                    Url = UrlHelper.ApiUrl<PageApi>(nameof(PageApi.Deletes))
                },
                Visible = Comparison.EqualOrGreaterThan(1)
            });

            meta.Menu.Add(new IconMenu
            {
                Align = MenuAlign.Right,
                IconClass = "gear",
                Action = ActionMeta.EmbeddedPopup(CreateRoutePopupMeta())
            });
            #endregion

            #region table
            meta.Builder<PageViewModel>()
                //.MergeModel()
                .Column<TextCell>(p => p.Name, new Localizable("Name"), null)
                .Column<BadgeCell>(p => p.Linked, new Localizable("Linked"), null)
                .Column<LabelCell>(o => o.Online, new Localizable("Online"), cell =>
                {
                    cell.Class = Class.UseCondition(Condition.Boolean("green", "blue"));
                    cell.Text = Format.UseCondition(Condition.Boolean("{online.yes}", "{online.no}"));
                })
                .Column<ArrayCell>(o => o.Relations, new Localizable("Relations"), cell =>
                {
                    cell.Text = new Localizable("{0} {key}");//todo confirm
                    cell.Action = ActionMeta.EmbeddedPopup(MetaHelper.CreateRelationPopupMeta(), "?id={id}&by={key}&type=page");//todo confirm
                })
                .Column<DateCell>(o => o.LastModified, new Localizable("Last modified"), null)
                .Column<LinkCell>(o => o.PreviewUrl, new Localizable("Preview"), cell =>
                {
                    cell.Action = new ActionMeta
                    {
                        Type = ActionType.NewWindow
                    };
                })
                .Column<ButtonCell>("setting", new Localizable(""), cell =>
                {
                    cell.Class = "btn-primary hidden-xs";
                    cell.Text = new Localizable("Setting");
                    cell.Action = new ActionMeta
                    {
                        Type = ActionType.Redirect,
                        Url = "/_admin/page/details?id={id}"
                    };
                })
                 .Column<ButtonCell>(o => o.InlineUrl, new Localizable(""), cell =>
                 {
                     cell.Class = "btn-primary hidden-xs";
                     cell.Text = new Localizable("Inline edit");
                     cell.Action = new ActionMeta
                     {
                         Type = ActionType.NewWindow
                     };
                 })
                .Column<IconCell>("qrCode", new Localizable("qrCode"), cell =>
                {
                    cell.Class = "btn-xs";
                    cell.IconClass = "qrcode";
                    cell.Action = new ActionMeta
                    {
                        Type = ActionType.Event,
                        Url = "showQrcode"
                    };
                });
            #endregion
        }

        #region popup
       

        private PopupMeta CreateRoutePopupMeta()
        {
            PopupMeta popupMeta = new PopupMeta();
            popupMeta.Title = new Localizable("Route setting");
            popupMeta.Description = new Description
            {
                Title = new Localizable("Redirect routes"),
                Content = new Localizable("Set the redirect pages for default home page, 404 and error pages"),
                Closable = true,
                Class = "alert alert-info alert-dismissable"
            };
            popupMeta.Buttons.Add(new PopupButton
            {
                Class = "green",
                Text = new Localizable("save"),
                Type = PageButtonAction.Submit
            });
            popupMeta.Buttons.Add(new PopupButton
            {
                Class = "gray",
                Text = new Localizable("cancel"),
                Type = PageButtonAction.Close
            });

            var meta = new FormMeta();
            meta.LoadApi = UrlHelper.ApiUrl<PageApi>(nameof(PageApi.DefaultRoute));
            meta.SubmitApi =UrlHelper.ApiUrl<PageApi>(nameof(PageApi.DefaultRouteUpdate));
            meta.Layout = FormLayout.Horizontal;

            meta.Builder<RouteFormMeta>()
                .Item(i => i.startPage, item =>
                {
                    item.Type = "selection";
                    item.Label = new Localizable("Home page");
                    item.Name = "startPage";
                    item.Options = SelectOptions.UseContext("context", "{name}", "{id}");


                })
                .Item(i => i.notFound, item =>
                {
                    item.Type = "selection";
                    item.Label = new Localizable("404 page");
                    item.Name = "notFound";
                    item.Options = SelectOptions.UseContext("context", "{name}", "{id}",
                        SelectOptions.UseDefaultOption("System default", Guid.Empty.ToString()));
                })
                .Item(i => i.error, item =>
                {
                    item.Type = "selection";
                    item.Label = new Localizable("Error page");
                    item.Name = "error";
                    item.Options = SelectOptions.UseContext("context", "{name}", "{id}",
                       SelectOptions.UseDefaultOption("System default", Guid.Empty.ToString()));
                })
                ;
            popupMeta.Views.Add(meta);

            return popupMeta;
        }

        #endregion
        class RouteFormMeta
        {
            public string startPage { get; set; }

            public string notFound { get; set; }

            public string error { get; set; }
        }
    }
}
