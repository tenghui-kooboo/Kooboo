using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Data.Context;

namespace Kooboo.Model.Components.Table
{
    public interface ICell
    {
        string ColumnName { get; set; }

        CellType CellType { get; }

        CellDataType CellDataType { get; set; }

        RenderContext Context { get; set; }

        object GetData();

        void SetData(List<Attribute> attrs);
    }
}
