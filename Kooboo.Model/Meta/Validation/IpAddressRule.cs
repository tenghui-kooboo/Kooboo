using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Kooboo.Model.Meta.Validation
{
    public class IpAddressRule:ValidationRule
    {
        public IpAddressRule(string message)
        {
            Message = message;
        }

        public override string GetRule()
        {
            return string.Format("{{type:\"ipAddress\",message:\"{0}\"}}", Message);
        }

        public override bool IsValid(object value)
        {
            if (base.IsValid(value))
            {
                return true;
            }

            return Regex.IsMatch(value.ToString(), "");
        }
    }
}
