using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Components.Table
{
    public class LinkCell : ICell
    {
        public CellType CellType => CellType.Link;

        public CellDataType CellDataType { get; set; }

        public string ColumnName { get; set; }

        public string Url { get; set; }

        public string[] Paras { get; set; }

        public object GetData()
        {
            var dic = new Dictionary<string, object>();
            dic.Add("columnName", ColumnName);
            dic.Add("cellType", CellType.ToString());
            dic.Add("url", Url);
            dic.Add("paras", Paras);

            return dic;
        }
    }
}
