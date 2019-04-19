using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{

    [AttributeUsage(AttributeTargets.Property)]
    public class Popup : Attribute, IMetaAttribute
    {
        public string PropertyName => "popup";

        public bool IsHeader => false;

        public string Title { get; set; }
        public bool ShowCloseBtn { get; set; }
        public string CloseBtnText { get; set; }

        public Popup(string title, bool showCloseBtn, string closeBtnText,params PopupButton[] buttons)
        {
            Title = title;
            ShowCloseBtn = showCloseBtn;
            CloseBtnText = closeBtnText;
        }

        public object Value()
        {
            var dic = new Dictionary<string, object>();
            dic.Add("title", Title);
            dic.Add("showCloseBtn", ShowCloseBtn);
            dic.Add("closeBtnText", CloseBtnText);

            return dic;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PopupButton:Attribute, IMetaAttribute
    {
        public string ClassName { get; set; }

        public string Text { get; set; }

        public EnumPopupBtnType Type { get; set; }

        public PopupButton(string className,string text, EnumPopupBtnType type)
        {
            ClassName = className;
            Text = text;
            Type = type;
        }
    }


    public enum EnumPopupBtnType
    {
        CloseModal,
        SumbitData
    }



}
