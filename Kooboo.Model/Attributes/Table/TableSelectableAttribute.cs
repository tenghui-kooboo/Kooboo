using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Attributes
{
    public class TableSelectableAttribute:Attribute
    {
        public bool Selectable { get; set; }

        public TableSelectableAttribute(bool selectable)
        {
            Selectable = selectable;
        }
    }
}
