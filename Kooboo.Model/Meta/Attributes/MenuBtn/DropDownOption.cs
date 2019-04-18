using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DropDownOption : Attribute, IMetaAttribute
    {
        public string Data { get; set; }

        public string DisplayName { get; set; }
        public DropDownOption(string data,string displayName)
        {
            Data = data;
            DisplayName = displayName;
        }

        public bool IsHeader => false;

        public string PropertyName => "Action";

        public string Value()
        {
            return $"{{data:\"{Data}\",displayName:{DisplayName}}}";
        }
    }
}
