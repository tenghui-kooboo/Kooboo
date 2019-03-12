using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Components.Table
{
    public class TextCell : ICell
    {
        public string ColumnName { get; set; }

        public CellType CellType => CellType.Text;

        public CellDataType CellDataType { get; set; }

        public object GetData()
        {
            var dic = new Dictionary<string, object>();
            dic.Add("columnName", ColumnName);
            dic.Add("cellDataType", CellDataType.ToString());

            return dic;
        }

        public void SetData(List<Attribute> attrs)
        {
            
        }
    }
}
