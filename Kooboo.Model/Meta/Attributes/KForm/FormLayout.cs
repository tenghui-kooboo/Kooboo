using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Model.Meta.Definition;

namespace Kooboo.Model.Meta.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FormLayoutAttribute : Attribute, IMetaAttribute
    {
        public string PropertyName => "layout";

        public bool IsHeader => false;

        public FormLayout Layout { get; set; }

        public FormLayoutAttribute(FormLayout layout)
        {
            Layout = layout;
        }

        public string Value()
        {
            return Layout.ToString();
        }

    }
}
