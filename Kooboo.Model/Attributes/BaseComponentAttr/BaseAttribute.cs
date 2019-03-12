using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Components;

namespace Kooboo.Model.Attributes
{
    public class BaseAttribute:Attribute
    {
        public bool Disabled { get; set; }

        public object FieldValue { get; set; }

        public string Tooltip { get; set; }

        public bool IsMultilingual { get; set; }

        public ComponentType ComponentType { get; set; }

        public BaseAttribute(bool disabled, object fieldValue,string tooltip,bool isMultilingual)
        {
            Disabled = disabled;
            FieldValue = fieldValue;
            Tooltip = tooltip;
            IsMultilingual = isMultilingual;
        }
    }
}
