using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Table
{
    public class ArrayColumnAttribute : ColumnAttribute
    {
        public ArrayColumnAttribute(string header)
            : base(header, typeof(ArrayCell))
        {
        }

        public ArrayColumnAttribute()
            : this(null)
        {
        }
    }
}
