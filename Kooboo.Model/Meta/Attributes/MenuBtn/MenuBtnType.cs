using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MenuBtnTypeAttribute : Attribute, IMetaAttribute
    {
        public EnumMenuBtnType MenuBtnType { get; set; }

        public MenuBtnTypeAttribute(EnumMenuBtnType btnType)
        {
            MenuBtnType = btnType;
        }

        public bool IsHeader => false;

        public string PropertyName => "type";

        public string Value()
        {
            return MenuBtnType.ToString();
        }
    }

    public enum EnumMenuBtnType
    {
        Btn=0,
        Dropdown=1
    }
}
