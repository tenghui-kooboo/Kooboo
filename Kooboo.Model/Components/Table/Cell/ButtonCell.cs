using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Attributes;
using Kooboo.Data.Context;
namespace Kooboo.Model.Components.Table
{
    //temp only support link btn
    public class ButtonCell : ICell
    {
        public string ColumnName { get; set; }

        public CellType CellType => CellType.Button;

        public CellDataType CellDataType { get; set; }

        public RenderContext Context { get; set; }

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

        public void SetData(List<Attribute> attrs)
        {
            //temp is same with link
            var linkAttr = attrs.Find(a => a is LinkAttribute) as LinkAttribute;
            if (linkAttr != null)
            {
                Url = linkAttr.Url;
                Paras = linkAttr.Paras;
            }
        }
    }
}
