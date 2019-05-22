using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Kooboo.Data.Language;

namespace Kooboo.Model.Meta.Validation
{
    public class IntegerRule:ValidationRule
    {
        public IntegerRule(string message)
        {
            Message = message;
        }

        public string _message;
        public override string Message
        {
            get
            {
                _message = string.IsNullOrEmpty(_message)
                 ? Hardcoded.GetValue("invalid Integer", Context)
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
            return string.Format("{{type:\"integer\",message:\"{0}\"}}", Message);
        }

        public override bool IsValid(object value)
        {
            if (base.IsValid(value))
            {
                return true;
            }

            return Regex.IsMatch(value.ToString(), "(^[0-9]*$)|(^-[0-9]+$)");

        }
    }
}
