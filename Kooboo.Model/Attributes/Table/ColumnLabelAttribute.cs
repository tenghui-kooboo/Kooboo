using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Attributes
{
    public class ColumnLabelAttribute:Attribute
    {
        public string Text { get; set; }

        public string Class { get; set; }

        public string ConditionField { get; set; }

        public string TrueText { get; set; }

        public string FalseText { get; set; }

        public ColumnLabelAttribute(string text,string clas,string conditionField,string trueText,string falseText)
        {
            Text = text;
            Class = clas;
            ConditionField = conditionField;
            TrueText = trueText;
            FalseText = falseText;
        }
    }
}
