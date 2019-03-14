using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Attributes;
using Kooboo.Data.Context;

namespace Kooboo.Model.Components.Table
{
    public class SummaryCell : ICell
    {
        public string ColumnName { get; set; }

        public CellType CellType => CellType.Summary;

        public CellDataType CellDataType { get; set; }

        public RenderContext Context { get; set; }

        public string TitleField { get; set; }

        public string DescField { get; set; }

        public string Url { get; set; }

        public string[] Paras { get; set; }

        public object GetData()
        {
            var dic = new Dictionary<string, object>();

            dic.Add("titleField", TitleField);
            dic.Add("descField", DescField);
            dic.Add("url", Url);
            dic.Add("paras", Paras);

            return dic;
        }

        public void SetData(List<Attribute> attrs)
        {
            var summaryAttr = attrs.Find(a => a is ColumnSummaryAttributeAttribute) as ColumnSummaryAttributeAttribute;
            if (summaryAttr != null)
            {
                TitleField = summaryAttr.TitleField;
                DescField = summaryAttr.DescField;
                Url = summaryAttr.Url;
                Paras = summaryAttr.Paras;
            }

        }
    }
}
