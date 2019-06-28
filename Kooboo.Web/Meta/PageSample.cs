using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Model.Meta;
using Kooboo.Model.Meta.Table;
using Kooboo.Model.Meta.Form;
using Kooboo.Model.Meta.Popup;
using Kooboo.Sites.Models;
using Kooboo.Web.Api.Implementation;
using Kooboo.Web.ViewModel;

namespace Kooboo.Web.Meta
{
    public class Sample : ITableMetaConfigure<PageSample>
    {
        public void Configure(TableMeta meta)
        {
            meta.DataApi = UrlHelper.ApiUrl<PageApi>(nameof(PageApi.All));
            meta.ListName = "page";//dataapi's return data is({page:[],layouts:[]})
            #region menu 
            //normal button
            meta.Menu.Add(new ButtonMenu
            {
                Text = new Localizable("New page"),
                Class="green",
                Action=new ActionMeta()
                {
                    Type=ActionType.Redirect,
                    Url= "/_Admin/Page/EditPage"
                }
            });
            //dropdown
            meta.Menu.Add(new DropDownMenu
            {
                Text = new Localizable("New layout page"),
                Class = "green",
                Action = new ActionMeta
                {
                    Type = ActionType.Redirect,
                    Url = "/_Admin/Page/EditLayout?layoutId={id}"
                },
                Options = SelectOptions.UseContext("layouts", "{name}", null)
            });

            //button visible control and popup action
            meta.Menu.Add(new ButtonMenu
            {
                Text = new Localizable("Copy"),
                Class = "green",
                Action = ActionMeta.Popup(UrlHelper.PopupMetaUrl<CopyPopup>()),
                Visible = Comparison.OnSingleSelection
            });

            //button visible control and post action
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
                Visible = Comparison.OnSeletion
            });
            #endregion

            #region table
            //config different table cell
            meta.Builder<PageViewModel>()
                .Column<TextCell>(p => p.Name, new Localizable("Name"))
                .Column<BadgeCell>(p => p.Linked, new Localizable("Linked"))
                //cell with popup action
                .Column<ArrayCell>(o => o.Relations, new Localizable("Relations"), cell =>
                {
                    cell.Text = Localizable.Raw("{0} {key}");
                    cell.Action = ActionMeta.Popup(UrlHelper.PopupMetaUrl<RelationPopup>());
                })
                .Column<DateCell>(p => p.LastModified, new Localizable("Last modified"))
                //cell with newwindow action
                .Column<LinkCell>(o => o.PreviewUrl, new Localizable("Preview"), cell =>
                {
                    cell.Action = new ActionMeta
                    {
                        Type = ActionType.NewWindow
                    };
                });
            #endregion
            meta.ModelType = "Page";//this is example,so need set this value
        }
    }


    public class PageSample : Page
    {

    }

}
