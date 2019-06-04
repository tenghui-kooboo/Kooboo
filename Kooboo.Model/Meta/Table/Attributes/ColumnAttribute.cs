using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Table
{
    public abstract class ColumnAttribute : Attribute
    {
        protected ColumnAttribute(string header, Type cellType)
            : this(header, Activator.CreateInstance(cellType) as Cell)
        {
        }

        protected ColumnAttribute(string header, Cell cell)
        {
            Header = header;
            Cell = cell;
        }

        public string Header { get; set; }

        public Cell Cell { get; set; }
    }
}
