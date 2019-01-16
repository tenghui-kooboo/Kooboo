using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.ValidateRules
{
    public class Rule:Attribute
    {
        public virtual string Message { get; set; }

        public virtual string GetRule()
        {
            return string.Empty;
        }
    }
}
