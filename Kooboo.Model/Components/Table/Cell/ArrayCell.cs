using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Attributes;
using Kooboo.Data.Context;

namespace Kooboo.Model.Components.Table
{
    public class ArrayCell : ICell
    {
        public string ColumnName { get; set; }

        public CellType CellType => CellType.Array;

        public CellDataType CellDataType { get; set; }

        public bool IsShow { get; set; }

        public object Config { get; set; } = new object();

        public RenderContext Context { get; set; }

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
            var attr = attrs.Find(a => a is NewModalAttribute) as NewModalAttribute;
            if(attr!=null)
            {
                if(attr.ComponentType==ComponentType.KTable||
                    attr.ComponentType == ComponentType.KForm)
                {
                    var modelName = attr.ComponentModelName;
                    if (!string.IsNullOrEmpty(modelName))
                    {
                        var config = ModelHelper.GetVueFields(modelName,Context);
                        Config = config;
                    }
                }
            }
        }
    }
}
