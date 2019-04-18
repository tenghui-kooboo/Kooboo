using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{
    
    [AttributeUsage(AttributeTargets.Property)]
    public class HeaderDisplayName : Attribute, IMetaAttribute
    {
        public string name { get; set; }

        public bool IsHeader => true;

        public string PropertyName => "displayName";

        public HeaderDisplayName(string name)
        {
            this.name = name;
        }

        public string Value()
        {
            return this.name;
        }
    }


}
