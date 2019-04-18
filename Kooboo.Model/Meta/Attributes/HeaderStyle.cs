using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class HeaderStyle : Attribute, IMetaAttribute
    {
        public string Style { get; set; }

        public bool IsHeader => true;

        public string PropertyName => "style";

        public HeaderStyle(string style)
        {
            this.Style = style;
        }

        public object Value()
        {
            return this.Style;
        }
    }
}
