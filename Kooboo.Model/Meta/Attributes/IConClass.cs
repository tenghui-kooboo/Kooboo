using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IConClass:Attribute, IMetaAttribute
    {
        public string PropertyName => "iconClass";

        public bool IsHeader => false;

        public string Class { get; set; }
        public IConClass(string iconClass)
        {
            Class = iconClass;
        }

        public string Value()
        {
            return Class;
        }
    }
}
