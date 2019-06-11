using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Model.Meta.Popup;
using Kooboo.Model.Meta.Table;
using Kooboo.Model.Meta.Form;
using Kooboo.Model.Meta;
using Kooboo.Web.Api.Implementation;
using Kooboo.Model.Meta.Validation;

namespace Kooboo.Web.Meta
{
    public class MetaHelper
    {
        public static PopupMeta CreateCopyPopupMeta()
        {
            PopupMeta popupMeta = new PopupMeta();
            popupMeta.Title = new Localizable("Copy");
            popupMeta.Buttons.Add(new PopupButton
            {
                Class = "green",
                Text = new Localizable("start"),
                Type = PageButtonAction.Submit
            });
            popupMeta.Buttons.Add(new PopupButton
            {
                Class = "gray",
                Text = new Localizable("cancel"),
                Type = PageButtonAction.Close
            });

            var meta = new FormMeta();
            meta.LoadApi = "";
            meta.SubmitApi = UrlHelper.ApiUrl<PageApi>(nameof(PageApi.Copy));
            meta.Layout = FormLayout.Horizontal;

            meta.Builder<CopyForm>()
                .Item(i => i.name, item =>
                {
                    item.Type = "textBox";
                    item.Label = new Localizable("name");
                    item.Class = "col-md-9";
                    item.Options = SelectOptions.UseContext("selected", "{name}_copy");

                    item.Rules.Add(new RequiredRule("required"));
                    item.Rules.Add(new MinLengthRule(1));
                    item.Rules.Add(new MaxLengthRule(64));
                    item.Rules.Add(new UniqueRule("/page/isUniqueName?name={name}", "taken"));
                })
                .Item(i => i.id, item =>
                {
                    item.Type = "hidden";
                    item.Label = new Localizable("id");
                    item.Name = "id";
                    item.Options = SelectOptions.UseContext("selected", "{id}");
                })
                .Item(i => i.url, item =>
                {
                    item.Type = "textBox";
                    item.Label = new Localizable("url");
                    item.Name = "url";
                    item.Class = "col-md-9";
                    item.Options = SelectOptions.UseContext("selected", "/{name}_copy");
                    item.Rules.Add(new RequiredRule("required"));
                    //item.Rules.Add(new RegexRule(".+", "invalid"));
                });
            popupMeta.Views.Add(meta);

            return popupMeta;
        }

        /// <summary>
        /// create relation popup
        /// </summary>
        /// <returns></returns>
        public static PopupMeta CreateRelationPopupMeta()
        {
            PopupMeta popupMeta = new PopupMeta();
            popupMeta.Title = new Localizable("Relation");
            popupMeta.Buttons.Add(new PopupButton
            {
                Class = "green",
                Text = new Localizable("OK"),
                Type = PageButtonAction.Close
            });

            var meta = new TableMeta();
            meta.DataApi = UrlHelper.ApiUrl<RelationApi>(nameof(RelationApi.ShowBy));
            //meta.DataApi = "Relation/ShowBy?id={id}&by={by}&type={type}";
            meta.ListName = "";
            meta.ShowSelected = false;
            meta.Builder<RelationTable>()
                .Column<LinkCell>(o => o.name, new Localizable("Name"), cell =>
                {
                    cell.Action = ActionMeta.NewWindow("{url}");
                })
                  .Column<TextCell>(o => o.remark, new Localizable("Remark"), null)
                  .Column<IconCell>("Edit", new Localizable("Edit"), cell =>
                  {
                      cell.Class = "blue btn-xs";
                      cell.IconClass = "pencil";
                      cell.Action = ActionMeta.NewWindow("/_Admin/Development/{by}?id={objectId}");
                  })
                  ;

            popupMeta.Views.Add(meta);

            return popupMeta;
        }

        class RelationTable
        {
            public string name { get; set; }

            public string remark { get; set; }
        }

        class CopyForm
        {
            public string name { get; set; }

            public string id { get; set; }

            public string url { get; set; }

        }

    }
}
