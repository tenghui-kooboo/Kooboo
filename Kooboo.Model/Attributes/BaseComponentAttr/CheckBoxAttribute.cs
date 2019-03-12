using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Attributes.BaseComponentAttr
{
    public class CheckBoxAttribute:Attribute
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public CheckBoxAttribute(string key,string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
