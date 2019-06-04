using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Table
{
    public class LabelColumnAttribute : ColumnAttribute
    {
        public LabelColumnAttribute(string header)
            : base(header, typeof(LabelCell))
        {
        }

        public LabelColumnAttribute()
            : this(null)
        {
        }
    }
}
