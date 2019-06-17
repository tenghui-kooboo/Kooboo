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

        public MenuOrder Order { get; set; } = MenuOrder.Right;

        public string Type { get; set; }

        public string Name { get; set; }

        public Localizable Text { get; set; }

        public MenuAlign Align { get; set; }

        public string Style { get; set; }

        public Class Class { get; set; }

        public Comparison Visible { get; set; }
    }

    public enum MenuAlign
    {
        Left,

        Right
    }

    public enum MenuOrder
    {
        Left,

        Center,

        Right
    }
}
