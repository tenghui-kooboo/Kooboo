using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Attributes;
using Kooboo.Data.Context;

namespace Kooboo.Model.Components.Table
{
    public class IconTextCell : ICell
    {
        public string ColumnName { get; set; }

        public CellType CellType => CellType.IconText;

        public CellDataType CellDataType { get; set; }

        public RenderContext Context { get; set; }

        public string Icon { get; set; }

        public string Text { get; set; }

        public string Title { get; set; }

        public object GetData()
        {
            var dic = new Dictionary<string, object>();
            dic.Add("icon", Icon);
            dic.Add("text", Text);
            dic.Add("title", Title);

            return dic;
        }

        public void SetData(List<Attribute> attrs)
        {
            var attr = attrs.Find(a => a is ColumnIconTextAttribute) as ColumnIconTextAttribute;
            if (attr != null)
            {
                Text = attr.Text;
                Icon = attr.Icon;
                Title = attr.Title;
            }
        }
    }
}
