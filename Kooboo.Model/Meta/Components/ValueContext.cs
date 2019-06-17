using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta
{
    public class ValueContext
    {
        public string DataField { get; set; }

        public string Value { get; set; }

        public static ValueContext FromSelected(string value)
        {
            return new ValueContext { DataField = "selected", Value = value };
        }

        public static ValueContext FromRow(string value)
        {
            return new ValueContext { DataField = "row", Value = value };
        }

        public static ValueContext DirectValue(string value)
        {
            return new ValueContext {  Value = value };
        }
    }
}
