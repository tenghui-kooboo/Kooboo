using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Attributes
{
    public class ColumnIconTextAttribute:Attribute
    {
        public string Icon { get; set; }

        public string Text { get; set; }

        public string Title { get; set; }

        public ColumnIconTextAttribute(string icon,string text,string title)
        {
            Icon = icon;
            Text = text;
            Title = title;
        }
    }
}
