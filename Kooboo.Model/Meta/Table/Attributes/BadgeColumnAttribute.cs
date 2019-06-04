using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Table
{
    public class BadgeColumnAttribute : ColumnAttribute
    {
        public BadgeColumnAttribute(string header)
            : base(header, typeof(BadgeCell))
        {
        }

        public BadgeColumnAttribute()
            : this(null)
        {
        }
    }
}
