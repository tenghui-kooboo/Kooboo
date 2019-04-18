using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CellText : Attribute, IMetaAttribute
    {
        public bool IsHeader => false;

        public string PropertyName => "condition";

        public string Text { get; set; }

        public CellText(string text)
        {
            Text = text;
        }

        public object Value()
        {
            return Text;
        }
    }
}
