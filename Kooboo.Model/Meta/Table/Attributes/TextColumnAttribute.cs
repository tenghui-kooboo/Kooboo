using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Table
{
    public class TextColumnAttribute : ColumnAttribute
    {
        public TextColumnAttribute(string header)
            : base(header, typeof(TextCell))
        {
        }

        public TextColumnAttribute()
            : this(null)
        {
        }
    }
}
