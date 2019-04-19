using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Model.Meta.Attributes;
using Kooboo.Model.Meta.Definition;

namespace Kooboo.Model.Setting
{
    [Relation(typeof(PageMenuModel),"/page/all","")]
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
        [Url("popup?modelname=pageCopyForm&id={id}")]
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
        [Url("popup?modelname=routeSettingForm")]
        public string RouteSetting { get; set; }
    }

    [Relation(typeof(CopyPopup),"","")]
    [FormLayout(FormLayout.Horizontal)]
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

    public class CopyPopup : IPopup
    {
        public string Title { get; set; } = "Copy";

        public bool ShowCancelBtn { get; set; } = true;

        public string CancelBtnText { get; set; } = "cancel";

        private List<PopupButton> buttons = new List<PopupButton>();

        public List<PopupButton> Buttons
        {
            get
            {
                buttons.Add(new PopupButton() { Class = "btn-primary", Text = "save", Type = EnumPopupButtonType.Submit });
                return buttons;
            }
        }
    }

    public class RouteSettingPopup : IPopup
    {
        public string Title { get; set; } = "RouteSetting";

        public bool ShowCancelBtn { get; set; } = true;

        public string CancelBtnText { get; set; } = "cancel";

        private List<PopupButton> buttons = new List<PopupButton>();

        public List<PopupButton> Buttons
        {
            get
            {
                buttons.Add(new PopupButton() { Class = "btn-primary", Text = "save", Type = EnumPopupButtonType.Submit });
                return buttons;
            }
        }
    }

    [Relation(typeof(RouteSettingPopup), "", "")]
    [FormLayout(FormLayout.Horizontal)]
    public class RouteSettingForm : IKoobooModel
    {
        public ModelType ModelType => ModelType.KForm;

        public string ModelName => "routeSettingForm";

        [FormType(EnumFormType.selection)]
        [FormLabel("Home page")]
        [FormPlaceholder("")]
        //[FormOption("path","id",null)]
        [FormOption("path", "id")]
        public string defaultPage{ get; set; }
        [FormType(EnumFormType.selection)]
        [FormLabel("404 page")]
        [FormPlaceholder("")]
        [FormOption("path", "id")]
        //[FormOption("path", "id",new DefaultOption("System default", Guid.Empty.ToString()))]
        public string notFoundPage{ get; set; }

        [FormType(EnumFormType.selection)]
        [FormLabel("Error page")]
        [FormPlaceholder("")]
        //[FormOption("path", "id")]
        [FormOption("path", "id")]
        public string errorPage{ get; set; }


    }

    //public class

}
