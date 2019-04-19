using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Definition
{
    public class KPopup
    {
        public string Title { get; set; }

        public bool ShowCloseBtn { get; set; }
        
        public string CloseBtnText { get; set; }

        public List<PopupButton> Btns { get; set; }
    }

    public class PopupButton
    {
        public string Class { get; set; }

        public string Text { get; set; }

        public EnumPopupButtonType Type { get; set; }

        public string TypeStr
        {
            get
            {
                return MetaHelper.ToCamalCaseName(Type.ToString());
            }
        }
    }
    
    public enum EnumPopupButtonType
    {
        Close,
        Submit
    }
}
