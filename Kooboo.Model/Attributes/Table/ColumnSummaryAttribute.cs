using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Attributes
{
    public class ColumnSummaryAttributeAttribute:Attribute
    {
        public string TitleField { get; set; }

        public string DescField { get; set; }

        public string Url { get; set; }

        public string[] Paras { get; set; }

        public ColumnSummaryAttributeAttribute(string titleField,string descField,string url,params string[] paras)
        {
            TitleField = titleField;
            DescField = descField;
            Url = url;
            Paras = paras;
        }
    }
}
