using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Model.Meta.Popup;
using Kooboo.Model.Meta.Table;
using Kooboo.Model.Meta.Form;
using Kooboo.Model.Meta.Validation;
using Kooboo.Web.Api.Implementation;
using Kooboo.Model.Meta;
using Kooboo.Data.Interface;

namespace Kooboo.Web.Meta
{
    public class CopyPopupMeta : IPopupMetaConfigure<CopyPopup>
    {
        public bool IsCreator => true;

        public void Configure(PopupMeta popupMeta)
        {
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
            //meta.SubmitApi = UrlHelper.ApiUrl<PageApi>(nameof(PageApi.Copy));
            //modelType bind by front
            meta.SubmitApi = "/{modelType}/copy";
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
                    item.Rules.Add(new UniqueRule("/{modelType}/isUniqueName?name={name}", "taken"));
                    //item.Rules.Add(new UniqueRule(UrlHelper.ApiUrl<PageApi>(nameof(PageApi.IsUniqueName)), "taken"));
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
        }
    }

    class CopyPopup : ISiteObject
    {
        public byte ConstType { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModified { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    class CopyForm
    {
        public string name { get; set; }

        public string id { get; set; }

        public string url { get; set; }

    }
}
