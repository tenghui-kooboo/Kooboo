using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Attributes
{
    public class ColumnIconAttribute:Attribute
    {
        public string Class { get; set; }

        public string Title { get; set; }

        public ColumnIconAttribute(string clas,string title)
        {
            Class = clas;
            Title = title;
        }
    }
}
