using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta
{
    public class Comparison
    {
        public static readonly Comparison OnMultipleSelection = Comparison.GreaterThan(1);
        public static readonly Comparison OnSingleSelection = Comparison.Equal(1);
        public static readonly Comparison OnSeletion = Comparison.EqualOrGreaterThan(1);

        public string Operator { get; set; }

        public object Value { get; set; }

        public static Comparison GreaterThan(object value)
        {
            return new Comparison { Operator = ">", Value = value };
        }

        public static Comparison EqualOrGreaterThan(object value)
        {
            return new Comparison { Operator = ">=", Value = value };
        }

        public static Comparison Equal(object value)
        {
            return new Comparison { Operator = "=", Value = value };
        }

        public static Comparison EqualOrLessThan(object value)
        {
            return new Comparison { Operator = "<=", Value = value };
        }

        public static Comparison LessThan(object value)
        {
            return new Comparison { Operator = "<", Value = value };
        }
    }
}
