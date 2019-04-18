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
        [CellType(EnumCellType.Badge)]
        [CssClass("badge-primary")]
        public string Linked { get; set; }

        [HeaderDisplayName("Online")]
        [HeaderClass("")]
        [CellType(EnumCellType.Label)]
        [LabelClass("","label-success", "label-info")]
        [LabelText("{online.yes}", "{online.no}")]
        public string Online { get; set; }

        [HeaderDisplayName("References")]
        [HeaderClass("")]
        [CellType(EnumCellType.Array)]
        [CellText("{0} {key}")]
        public string Relations { get; set; }

        [HeaderDisplayName("Last Modified")]
        [HeaderClass("")]
        [CellType(EnumCellType.Date)]
        public string LastModified { get; set; }

        [HeaderDisplayName("Preview")]
        [HeaderClass("")]
        [CellType(EnumCellType.Link)]
        [ActionType(EnumActionType.NewWindow)]
        public string PreviewUrl { get; set; }

        [CellType(EnumCellType.Button)]
        [CssClass("blue")]
        [InnerText("Setting")]
        [ActionType(EnumActionType.Redirect)]
        [Url("/_Admin/Page/EditRedirector?id={id}")]
        public string Setting { get; set; }

        [CellType(EnumCellType.Button)]
        [CssClass("blue")]
        [InnerText("Inline edit")]
        [ActionType(EnumActionType.NewWindow)]
        public string InlineUrl { get; set; }

        [CellType(EnumCellType.Icon)]
        [IConClass("qrcode")]
        [ActionType(EnumActionType.Event)]
        [Url("openQrcode")]
        public string QrCode { get; set; }

    }

    public class PageMenuModel:IKoobooModel,IKMenu
    {
        public ModelType ModelType => ModelType.KMenu;

        public string ModelName => "PageMenu";

        [MenuBtnType(EnumMenuBtnType.Button)]
        [DisplayName("New Page")]
        [CssClass("green")]
        [ActionType(Meta.Definition.EnumActionType.NewWindow)]
        [Url("/_Admin/Page/EditPage")]
        public string Create { get; set; }

        [MenuBtnType(EnumMenuBtnType.Button)]
        [DisplayName("New rich text page")]
        [CssClass("green")]
        [ActionType(Meta.Definition.EnumActionType.NewWindow)]
        [Url("/_Admin/Page/EditRichText")]
        public string NewRichPage { get; set; }

        [MenuBtnType(EnumMenuBtnType.Dropdown)]
        [DisplayName("New layout page")]
        [CssClass("green")]
        [ActionType(Meta.Definition.EnumActionType.NewWindow)]
        [Url("/_Admin/Page/EditLayout")]
        [DropDownOption("layouts","name")]
        public string NewLayoutPage { get; set; }

        [MenuBtnType(EnumMenuBtnType.Button)]
        [DisplayName("Import")]
        [CssClass("green")]
        [ActionType(Meta.Definition.EnumActionType.Popup)]
        [Url("details?modelname=xx")]
        public string Import { get; set; }

        [MenuBtnType(EnumMenuBtnType.Button)]
        [DisplayName("Copy")]
        [CssClass("green")]
        [ActionType(Meta.Definition.EnumActionType.Popup)]
        [Url("Detail?modelname=pageCopyForm&id={id}")]
        [Visible(CompareOperation.Equal, 1)]
        public string Copy { get; set; }

        [MenuBtnType(EnumMenuBtnType.Button)]
        [DisplayName("Delete")]
        [CssClass("red")]
        [ActionType(Meta.Definition.EnumActionType.Post)]
        [Url("/page/delete")]
        [Visible(CompareOperation.EqualOrGreaterThan, 1)]
        public string Delete { get; set; }

        [MenuBtnType(EnumMenuBtnType.Icon)]
        [DisplayName("Route Setting")]
        [CssClass("nav-btn pull-right")]
        [IConClass("gear")]
        [ActionType(Meta.Definition.EnumActionType.Popup)]
        [Url("routeSetting")]
        public string RouteSetting { get; set; }
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
