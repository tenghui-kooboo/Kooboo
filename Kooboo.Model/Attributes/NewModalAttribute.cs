using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Components;

namespace Kooboo.Model.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class NewModalAttribute:Attribute
    {
        public string Title { get; set; }

        public ComponentType ComponentType { get; set; }

        public string ComponentModelName { get; set; }

        public NewModalAttribute(string title, ComponentType componentType,string componentModelName)
        {
            Title = title;
            ComponentType = componentType;
            ComponentModelName = componentModelName;
        }
    }
}
