using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{

    [AttributeUsage(AttributeTargets.Property)]
    public class FormPlaceholder : Attribute, IMetaAttribute
    {
        public string PropertyName => "placeholder";

        public bool IsHeader => false;

        public string Placeholder { get; set; }

        public FormPlaceholder(string placeholder)
        {
            Placeholder = placeholder;
        }

        public object Value()
        {
            return Placeholder;
        }

    }
}
