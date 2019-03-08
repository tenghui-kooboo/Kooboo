using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Components.Table.Cell
{
    public class IconCell : ICell
    {
        public string ColumnName { get; set; }

        public CellType CellType => CellType.Icon;

        public CellDataType CellDataType { get; set; }

        public object GetData()
        {
            throw new NotImplementedException();
        }
    }
}
