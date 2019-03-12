using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Components;

namespace Kooboo.Model.Attributes
{
    public class FormFieldAttribute:Attribute
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public ComponentType ComponentType { get; set; }

        public FormFieldAttribute(string name,string displayName, ComponentType componentType)
        {
            Name = name;
            DisplayName = displayName;
            ComponentType = componentType;
        }
    }
}
