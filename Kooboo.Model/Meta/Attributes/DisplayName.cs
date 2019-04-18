using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayNameAttribute : Attribute, IMetaAttribute
    {
        public string DisplayName { get; set; }

        public DisplayNameAttribute(string displayName)
        {
            DisplayName = displayName;
        }

        public bool IsHeader => false;

        public string PropertyName => "displayName";

        public object Value()
        {
            return DisplayName.ToString();
        }
    }
}
