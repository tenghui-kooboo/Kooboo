using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{

    [AttributeUsage(AttributeTargets.Property)]
    public class FormToolTip : Attribute, IMetaAttribute
    {
        public string PropertyName => "tooltip";

        public bool IsHeader => false;

        public string Tooltip { get; set; }

        public FormToolTip(string tooltip)
        {
            Tooltip = tooltip;
        }

        public object Value()
        {
            return Tooltip;
        }

    }
}
