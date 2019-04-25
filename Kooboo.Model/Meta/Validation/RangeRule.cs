using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Validation
{
    public class RangeRule : ValidationRule
    {
        public object From { get; set; }

        public object To { get; set; }
    }
}
