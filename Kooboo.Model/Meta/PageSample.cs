using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Model.Meta;
using Kooboo.Model.Meta.Form;
using Kooboo.Model.Meta.Table;
using Kooboo.Model.Meta.Validation;
using Kooboo.Model.Meta.Popup;

namespace Kooboo.Model.Meta
{

    public class PageSample : ITableMetaConfigure<Page>
    {
        public bool IsCreator =>true;

        public void Configure(TableMeta meta)
        {
            meta.DataApi ="/page/all";
            meta.ListName = "page";

            #region menu
            meta.Menu.Add(new ButtonMenu
            {
                Text =new Localizable("New page"),
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
                Action = new ActionMeta()
                {
                    Type = ActionType.Redirect,
                    Url = "/_Admin/Page/EditRichText"
                }
            });

            meta.Menu.Add(new DropDownMenu
            {
                Text = new Localizable("New layout page"),
                Class = "green",
                Action = new ActionMeta()
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
                Action = new ActionMeta()
                {
                    Type = ActionType.Popup,

                    Url = "popup?modelName=importPopup"
                }
            });

            meta.Menu.Add(new ButtonMenu()
            {
                Text = new Localizable("Copy"),
                Class = "green",
                Action=ActionMeta.EmbeddedPopup(CreateCopyPopupMeta()),
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
                    Url = "page/deletes"
                },
                Visible = Comparison.EqualOrGreaterThan(1)
            });

            meta.Menu.Add(new IconMenu
            {
                //Text = "Copy",
                Align = MenuAlign.Right,
                IconClass = "gear",
                Action=ActionMeta.EmbeddedPopup(CreateRoutePopupMeta())
            });
            #endregion

            #region table
            meta.Builder<PageViewModel>()
                //.MergeModel()
                .Column<TextCell>(p=>p.name, new Localizable("Name"), null)
                .Column<BadgeCell>(p=>p.linked, new Localizable("Linked"), null)
                .Column<LabelCell>(o => o.online, new Localizable("Online"), cell =>
                  {
                      cell.Class = Class.UseCondition(Condition.Boolean("green", "blue"));
                      cell.Text = Format.UseCondition(Condition.Boolean("{online.yes}", "{online.no}"));
                  })
                .Column<ArrayCell>(o => o.relations, new Localizable("Relations"), cell =>
                  {
                      cell.Text = new Localizable("{0} {key}");//todo confirm
                      cell.Action = ActionMeta.EmbeddedPopup(CreateRelationPopupMeta(), "?id={id}&by={key}&type=page");//todo confirm
                  })
                .Column<DateCell>(o => o.lastModified, new Localizable("Last modified"), null)
                .Column<LinkCell>(o => o.previewUrl, new Localizable("Preview"), cell =>
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
                 .Column<ButtonCell>(o => o.inlineUrl, new Localizable(""), cell =>
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
        private PopupMeta CreateCopyPopupMeta()
        {
            PopupMeta popupMeta = new PopupMeta();
            popupMeta.Title = "Copy";
            popupMeta.Buttons.Add(new PopupButton()
            {
                Class = "green",
                Text = "start",
                Type = PageButtonAction.Submit
            });
            popupMeta.Buttons.Add(new PopupButton()
            {
                Class = "gray",
                Text = "cancel",
                Type = PageButtonAction.Close
            });

            var meta = new FormMeta();
            meta.LoadApi = "";
            meta.SubmitApi = "/page/copy";
            meta.Layout = FormLayout.Horizontal;

            meta.Builder<CopyForm>()
                .MergeModel()
                .Item((i) => i.name, (FormItem item) =>
                {
                    item.Type = "textBox";
                    item.Label = "name";
                    item.Class = "col-md-9";
                    item.Options = SelectOptions.UseContext("selected", "{name}_copy");

                    item.Rules.Add(new RequiredRule("required"));
                    item.Rules.Add(new MinLengthRule(1));
                    item.Rules.Add(new MaxLengthRule(64));
                    item.Rules.Add(new UniqueRule("/page/isUniqueName?name={name}", "taken"));
                })
                .Item((i) => i.id, (FormItem item) =>
                {
                    item.Type = "hidden";
                    item.Label = "id";
                    item.Name = "id";
                    item.Options = SelectOptions.UseContext("selected", "{id}");
                })
                .Item((i) => i.url, (FormItem item) =>
                {
                    item.Type = "textBox";
                    item.Label = "url";
                    item.Name = "url";
                    item.Class = "col-md-9";
                    item.Options = SelectOptions.UseContext("selected", "/{name}_copy");
                    item.Rules.Add(new RequiredRule("required"));
                    //item.Rules.Add(new RegexRule(".+", "invalid"));
                })
                ;
            popupMeta.Views.Add(meta);

            return popupMeta;
        }

        //move to common method method
        private PopupMeta CreateRelationPopupMeta()
        {
            PopupMeta popupMeta = new PopupMeta();
            popupMeta.Title = "Relation";
            popupMeta.Buttons.Add(new PopupButton()
            {
                Class = "green",
                Text = "OK",
                Type = PageButtonAction.Close
            });

            var meta = new TableMeta();
            meta.DataApi = "Relation/ShowBy?id={id}&by={by}&type={type}";
            meta.ListName = "";
            meta.ShowSelected = false;
            meta.Builder<RelationTable>()
                .MergeModel()
                .Column<LinkCell>("name", new Localizable("Name"), (LinkCell cell) =>
                {
                    cell.Action = ActionMeta.NewWindow("{url}");

                })
                  .Column<TextCell>("remark", new Localizable("Remark"), null)
                  .Column<IconCell>("Edit", new Localizable("Edit"), (IconCell cell) =>
                  {
                      cell.Class = "blue btn-xs";
                      cell.IconClass = "pencil";
                      cell.Action = ActionMeta.NewWindow("/_Admin/Development/{by}?id={objectId}");
                  })
                  ;

            popupMeta.Views.Add(meta);

            return popupMeta;
        }

        private PopupMeta CreateRoutePopupMeta()
        {
            PopupMeta popupMeta = new PopupMeta();
            popupMeta.Title = "Route setting";
            popupMeta.Description = new Description()
            {
                Title = "Redirect routes",
                Content = "Set the redirect pages for default home page, 404 and error pages",
                Closable = true,
                Class = "alert alert-info alert-dismissable"
            };
            popupMeta.Buttons.Add(new PopupButton()
            {
                Class = "green",
                Text = "save",
                Type = PageButtonAction.Submit
            });
            popupMeta.Buttons.Add(new PopupButton()
            {
                Class = "gray",
                Text = "cancel",
                Type = PageButtonAction.Close
            });

            var meta = new FormMeta();
            meta.LoadApi = "/Page/DefaultRoute";
            meta.SubmitApi = "/page/defaultRouteUpdate";
            meta.Layout = FormLayout.Horizontal;

            meta.Builder<RouteFormMeta>()
                .MergeModel()
                .Item((i) => i.startPage, (FormItem item) =>
                {
                    item.Type = "selection";
                    item.Label = "Home page";
                    item.Name = "startPage";
                    item.Options = SelectOptions.UseContext("context", "{name}", "{id}");


                })
                .Item((i) => i.notFound, (FormItem item) =>
                {
                    item.Type = "selection";
                    item.Label = "404 page";
                    item.Name = "notFound";
                    item.Options = SelectOptions.UseContext("context", "{name}", "{id}",
                        SelectOptions.UseDefaultOption("System default", Guid.Empty.ToString()));
                })
                .Item((i) => i.error, item =>
                {
                    item.Type = "selection";
                    item.Label = "Error page";
                    item.Name = "error";
                    item.Options = SelectOptions.UseContext("context", "{name}", "{id}",
                       SelectOptions.UseDefaultOption("System default", Guid.Empty.ToString()));
                })
                ;
            popupMeta.Views.Add(meta);

            return popupMeta;
        }

        #endregion
    }

    class Page : Kooboo.Data.Interface.ISiteObject
    {
        public byte ConstType { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModified { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    class PageViewModel
    {
        //public Guid Id { get; set; }
        public string name { get; set; }

        //public string Title { get; set; }

        //public int Warning { get; set; }
        //[BadgeColumn("Linked")]
        public string linked { get; set; }

        //public int PageView { get; set; }

        //public Guid LayoutId { get; set; }
        //[LabelColumn("Online")]
        ////cell
        public string online { get; set; }

        //[ArrayColumn("Relations")]
        public Dictionary<string, int> relations { get; set; } = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        //[DateColumn("Last modified")]
        public string lastModified { get; set; }

        //public string Path { get; set; }
        //how to specify url from data
        //[LinkColumn("Preview",ActionType.NewWindow,null)]
        public string previewUrl { get; set; }

        public string inlineUrl { get; set; }

        //public bool StartPage { get; set; }
    
        //[JsonConverter(typeof(StringEnumConverter))]
        //public PageType Type { get; set; }

    }
    class CopyForm
    {
        //[RequiredRule("required")]
        //[MinLengthRule(1)]
        //[MaxLengthRule(64)]
        //[UniqueRule("/page/isUniqueName?name={name}","take")]
        public string name { get; set; }

        public string id { get; set; }

        public string url { get; set; }

        
    }
    class RelationTable { }

    class RouteFormMeta
    {
        public string startPage { get; set; }

        public string notFound { get; set; }

        public string error { get; set; }
    }
}
