using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.ValidateRules
{
    public class RequireRule:Rule
    {
        public RequireRule(string message)
        {
            Message = message;
        }

        public override string GetRule()
        {
            return string.Format("{{type:\"required\",message:\"{0}\"}}",Message);
        }
    }
}
