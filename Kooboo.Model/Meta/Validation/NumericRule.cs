using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Kooboo.Model.Meta.Validation
{
    public class NumericRule:ValidationRule
    {
        public NumericRule(string message)
        {
            Message = message;
        }

        public override string GetRule()
        {
            return string.Format("{{type:\"numeric\",message:\"{0}\"}}",Message);
        }

        public override bool IsValid(object value)
        {
            if (value == null||(value as string)?.Length==0)
                return true;

            return Regex.IsMatch(value.ToString(), "^[0-9]*$");
        }

    }
}
