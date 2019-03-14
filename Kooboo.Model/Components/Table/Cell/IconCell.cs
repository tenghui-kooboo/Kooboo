using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Attributes;
using Kooboo.Data.Context;

namespace Kooboo.Model.Components.Table
{
    public class IconCell : ICell
    {
        public string ColumnName { get; set; }

        public CellType CellType => CellType.Icon;

        public CellDataType CellDataType { get; set; }

        public RenderContext Context { get; set; }

        public string Class { get; set; }

        public string Title { get; set; }

        public object GetData()
        {
            var dic = new Dictionary<string, object>();
            dic.Add("class", Class);
            dic.Add("title", Title);

            return dic;
        }

        public void SetData(List<Attribute> attrs)
        {
            var attr = attrs.Find(a => a is ColumnIconAttribute) as ColumnIconAttribute;
            if (attr != null)
            {
                Class = attr.Class;
                Title = attr.Title;
            }
        }
    }
}
