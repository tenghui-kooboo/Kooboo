using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Attributes;

namespace Kooboo.Model.Components.Table
{
    public class LabelCell : ICell
    {
        public string ColumnName { get; set; }

        public CellType CellType => CellType.Label;

        public CellDataType CellDataType { get; set; }

        public string Text { get; set; }

        public string Class { get; set; }

        public string ConditionField { get; set; }

        public string TrueText { get; set; }

        public string FalseText { get; set; }

        public object GetData()
        {
            var dic = new Dictionary<string, object>();
            dic.Add("text", Text);
            dic.Add("class", Class);
            dic.Add("conditionField", ConditionField);
            dic.Add("trueText", TrueText);
            dic.Add("falseText", FalseText);

            return dic;
        }

        public void SetData(List<Attribute> attrs)
        {
            var attr = attrs.Find(a => a is ColumnLabelAttribute) as ColumnLabelAttribute;
            if (attr != null)
            {
                Text = attr.Text;
                Class = attr.Class;
                ConditionField = attr.ConditionField;
                TrueText = attr.TrueText;
                FalseText = attr.FalseText;
            }
        }
    }
}
