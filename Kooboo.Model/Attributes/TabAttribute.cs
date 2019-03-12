using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Components;

namespace Kooboo.Model.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class TabAttribute:Attribute
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public ComponentType ComponentType { get; set; }

        //some component need detail setting
        public string ModelName { get; set; }

        public TabAttribute(string name,string displayName,ComponentType componentType,string modelName="")
        {
            Name = name;
            DisplayName = displayName;
            ComponentType = componentType;
            ModelName = modelName;
        }
    }
}
