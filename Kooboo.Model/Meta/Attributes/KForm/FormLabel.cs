using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{

    [AttributeUsage(AttributeTargets.Property)]
    public class FormLabel : Attribute, IMetaAttribute
    {
        public string PropertyName => "label";

        public bool IsHeader => false;

        public string Text { get; set; }

        public FormLabel(string text)
        {
            Text = text;
        }

        public string Value()
        {
            return Text.ToString();
        }

    }
}
