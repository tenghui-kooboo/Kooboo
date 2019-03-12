using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Attributes;

namespace Kooboo.Model.Components.Table
{
    public class ArrayCell : ICell
    {
        public string ColumnName { get; set; }

        public CellType CellType => CellType.Array;

        public CellDataType CellDataType { get; set; }

        public bool IsShow { get; set; }

        public object Config { get; set; } = new object();

        public object GetData()
        {
            var dic = new Dictionary<string, object>();
            dic.Add("columnName", ColumnName);
            dic.Add("cellType", CellType.ToString());
            dic.Add("isShow", IsShow);
            dic.Add("config", Config);

            return dic;
        }

        public void SetData(List<Attribute> attrs)
        {
            //change the show key
            //var attr= attrs.Find(a => a is ModalAttribute) as ModalAttribute;
            //if(attr!=null)
            //{

            //}
        }
    }
}
