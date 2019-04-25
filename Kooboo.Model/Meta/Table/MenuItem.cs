using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Kooboo.Model.Meta.Table
{
    public abstract class MenuItem : INamedMeta
    {
        protected MenuItem()
        { 
            var typeName = GetType().Name.ToLower();
            Type = typeName.Substring(0, typeName.Length - 4);
        }

        protected MenuItem(string type)
        {
            Type = type;
        }

        public string Type { get; set; }

        public string Name { get; set; }

        public Localizable Text { get; set; }

        public MenuAlign Align { get; set; }

        public string Style { get; set; }

        public ClassMeta Class { get; set; }

        public ComparisonMeta Visible { get; set; }
    }

    public enum MenuAlign
    {
        Left,

        Right
    }
}
