using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Validation
{
    public class RequiredRule : ValidationRule
    {
        public RequiredRule()
        {
            Message = "Required";
        }
    }
}
