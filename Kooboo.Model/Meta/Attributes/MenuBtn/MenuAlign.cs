using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes.MenuBtn
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MenuAlign : Attribute, IMetaAttribute
    {
        public string Align { get; set; }

        public MenuAlign(string align)
        {
            Align = align;
        }

        public bool IsHeader => false;

        public string PropertyName => "align";

        public string Value()
        {
            return Align;
        }
    }
}
