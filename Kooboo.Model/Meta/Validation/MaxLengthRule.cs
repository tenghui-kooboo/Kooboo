using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Validation
{
    public class MaxLengthRule : ValidationRule
    {
        public MaxLengthRule(int maxLength)
        {
            MaxLength = maxLength;
        }

        public int MaxLength { get; set; }
    }
}
