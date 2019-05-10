using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Data.Language;

namespace Kooboo.Model.Meta.Validation
{
    public class RegexRule:ValidationRule
    {
        public string Regex { get; set; }
        public RegexRule(string regex,string message="")
        {
            Regex = regex;
            Message = message;
        }

        private string _message;
        public override string Message
        {
            get
            {
                _message = string.IsNullOrEmpty(_message)
                   ? Hardcoded.GetValue("invalid", Context)
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
            return string.Format("{{type:\"regex\",regex:\"{0}\",message:\"{1}\"}}",Regex, Message);
        }

        public override bool IsValid(object value)
        {
            if (base.IsValid(value))
            {
                return true;
            }

            return System.Text.RegularExpressions.Regex.IsMatch(value.ToString(), Regex);

        }
    }
}
