using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Model.Meta.Definition;

namespace Kooboo.Model.Meta.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CellSummaryData : Attribute, IMetaAttribute
    {

        public bool IsHeader => false;

        public string PropertyName => "data";
        public string[] Data { get; set; }

        public CellSummaryData(params string[] data)
        {
            this.Data = data;
        }

        public string Value()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            for(var i = 0; i < Data.Length; i++)
            {
                if (i > 0)
                {
                    sb.Append(",");
                }
                sb.AppendFormat("\"{0}\"", Data[i]);
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}
