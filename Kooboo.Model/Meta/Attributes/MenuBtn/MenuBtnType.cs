using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Model.Meta.Definition;

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

        public object Value()
        {
            return MetaHelper.ToCamalCaseName(MenuBtnType.ToString());
        }
    }

    public enum EnumMenuBtnType
    {
        Button=0,
        Dropdown=1,
        Icon=2
    }
}
