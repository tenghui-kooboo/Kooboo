using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Table
{
    public class DateColumnAttribute : ColumnAttribute
    {
        public DateColumnAttribute(string header)
            : base(header, typeof(DateCell))
        {
        }

        public DateColumnAttribute()
            : this(null)
        {
        }
    }
}
