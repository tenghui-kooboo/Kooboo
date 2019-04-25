using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta
{
    public class ComparisonMeta
    {
        public string Operator { get; set; }

        public object Value { get; set; }

        public static ComparisonMeta GreaterThan(object value)
        {
            return new ComparisonMeta { Operator = ">", Value = value };
        }

        public static ComparisonMeta EqualOrGreaterThan(object value)
        {
            return new ComparisonMeta { Operator = ">=", Value = value };
        }

        public static ComparisonMeta Equal(object value)
        {
            return new ComparisonMeta { Operator = "=", Value = value };
        }

        public static ComparisonMeta EqualOrLessThan(object value)
        {
            return new ComparisonMeta { Operator = "<=", Value = value };
        }

        public static ComparisonMeta LessThan(object value)
        {
            return new ComparisonMeta { Operator = "<", Value = value };
        }
    }
}
