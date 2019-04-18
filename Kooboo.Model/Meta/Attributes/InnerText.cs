using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class InnerText : Attribute, IMetaAttribute
    {
        public string Text { get; set; }

        public string PropertyName => "innerText";

        public bool IsHeader => false;

        public InnerText(string innerText)
        {
            Text = innerText;
        }

        public string Value()
        {
            return Text;
        }
    }
}
