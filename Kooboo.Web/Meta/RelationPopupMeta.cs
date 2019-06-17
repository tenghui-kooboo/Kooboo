using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Model.Meta.Popup;
using Kooboo.Model.Meta.Table;
using Kooboo.Web.Api.Implementation;
using Kooboo.Model.Meta;
using Kooboo.Data.Interface;

namespace Kooboo.Web.Meta
{
    public class RelationPopupMeta : IPopupMetaConfigure<RelationPopup>
    {
   
        public void Configure(PopupMeta popupMeta)
        {
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

        }
    }

    class RelationPopup : ISiteObject
    {
        public byte ConstType { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModified { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    class RelationTable
    {
        public string name { get; set; }

        public string remark { get; set; }
    }
}
