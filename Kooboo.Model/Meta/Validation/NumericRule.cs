using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Kooboo.Data.Language;

namespace Kooboo.Model.Meta.Validation
{
    public class NumericRule:ValidationRule
    {
        public NumericRule(string message="")
        {
            Message = message;
        }
        private string _message;
        public override string Message
        {
            get
            {
                _message = string.IsNullOrEmpty(_message)
                   ? Hardcoded.GetValue("invalid numeric", Context)
                    : Hardcoded.GetValue(_message, Context);

                return _message;
            }
            set
            {
                _message = value;
            }
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
