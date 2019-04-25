using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Validation
{
    public class MinLengthRule : ValidationRule
    {
        public MinLengthRule(int minLength)
        {
            MinLength = minLength;
        }

        public int MinLength { get; set; }
    }
}
