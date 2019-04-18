using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Model.Meta.Attributes;
using Kooboo.Model.Meta.Definition;

namespace Kooboo.Model.Setting
{
    [Relation(typeof(PageMenuModel),"/page/all")]
    public class PageModel : IKoobooModel
    {
        public ModelType ModelType { get; set; }

        public string ModelName =>"Page";

        [HeaderDisplayName("Name")]
        [HeaderClass("")]
        [CellType(EnumCellType.Text)]
        public string Name { get; set; }

        [HeaderDisplayName("Linked")]
        [HeaderClass("")]
        [CellType(EnumCellType.Badage)]
        [CssClass("badge badge-primary")]
        public string Linked { get; set; }

        [HeaderDisplayName("Online")]
        [HeaderClass("")]
        [CellType(EnumCellType.Label)]
        [LabelClass("label label-sm", "label-success", "label-info")]
        public string Online { get; set; }

        [HeaderDisplayName("References")]
        [HeaderClass("")]
        [CellType(EnumCellType.Array)]
        [CssClass("label label-sm kb-table-label-refer")]
        [CellText("{0} {key}")]
        public string Relations { get; set; }

        [HeaderDisplayName("Last Modified")]
        [HeaderClass("")]
        [CellType(EnumCellType.Date)]
        public string LastModified { get; set; }

        [HeaderDisplayName("Preview")]
        [HeaderClass("")]
        [CellType(EnumCellType.Link)]
        [ActionType(EnumActionType.Open)]
        public string Preview { get; set; }

        [CellType(EnumCellType.Button)]
        [CssClass("btn btn-sm blue")]
        [InnerText("Inline edit")]
        [ActionType(EnumActionType.Open)]
        [Url("/page/details?id={id}")]
        public string Setting { get; set; }

        [CellType(EnumCellType.Button)]
        [CssClass("btn btn-sm blue")]
        [InnerText("Inline edit")]
        [ActionType(EnumActionType.Open)]
        public string InlineUrl { get; set; }

        [CellType(EnumCellType.Button)]
        [CssClass("btn btn-xs btn-default")]
        [IConClass("fa fa-qrcode")]
        [ActionType(EnumActionType.Event)]
        [Url("delete")]
        public string QrCode { get; set; }
    }

    public class PageMenuModel:IKoobooModel,IKMenu
    {
        public ModelType ModelType => ModelType.KMenu;

        public string ModelName => "PageMenu";

        [MenuBtnType(EnumMenuBtnType.Btn)]
        [DisplayName("New Layout")]
        [CssClass("btn green navbar-btn")]
        [ActionType(Meta.Definition.EnumActionType.Open)]
        [Url("/_Admin/Page/EditPage")]
        public string Create { get; set; }

        [MenuBtnType(EnumMenuBtnType.Btn)]
        [DisplayName("New rich text page")]
        [CssClass("btn green navbar-btn")]
        [ActionType(Meta.Definition.EnumActionType.Open)]
        [Url("/_Admin/Page/EditRichText")]
        public string NewRichPage { get; set; }

        [MenuBtnType(EnumMenuBtnType.Dropdown)]
        [DisplayName("New layout page")]
        [CssClass("btn green navbar-btn")]
        [ActionType(Meta.Definition.EnumActionType.Open)]
        [Url("/_Admin/Page/EditLayout")]
        [DropDownOption("layouts","name")]
        public string NewLayoutPage { get; set; }

        [MenuBtnType(EnumMenuBtnType.Btn)]
        [DisplayName("Copy")]
        [CssClass("btn green navbar-btn")]
        [ActionType(Meta.Definition.EnumActionType.Popup)]
        [Url("Detail?modelname=pageCopyForm&id={id}")]
        public string Copy { get; set; }

        [MenuBtnType(EnumMenuBtnType.Btn)]
        [DisplayName("Delete")]
        [CssClass("btn red navbar-btn")]
        [ActionType(Meta.Definition.EnumActionType.Event)]
        [Url("delete")]
        public string Delete { get; set; }
    }

    public class PageCopyForm : IKoobooModel
    {
        public ModelType ModelType=>ModelType.KForm;

        public string ModelName => "pageCopyForm";

        [FormType(EnumFormType.textbox)]
        [FormLabel("Name")]
        [FormPlaceholder("")]
        public string Name { get; set; }
        [FormType(EnumFormType.textbox)]
        [FormLabel("Url")]
        [FormPlaceholder("")]
        public string Url { get; set; }
    }

}
