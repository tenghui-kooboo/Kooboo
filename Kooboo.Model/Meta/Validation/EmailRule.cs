using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Validation
{
    public class EmailRule : RegexRule
    {
        public EmailRule()
            : base("^[-!#$%&\'*+\\./0-9=?A-Z^_`a-z{|}~]+@[-!#$%&\'*+\\/0-9=?A-Z^_`a-z{|}~]+\\.[-!#$%&\'*+\\./0-9=?A-Z^_`a-z{|}~]+$")
        {
        }
    }
}
