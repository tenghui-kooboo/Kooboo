using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Data.Language;

namespace Kooboo.Model.Meta.Validation
{
    public class RequiredRule : ValidationRule
    {
        public RequiredRule(string message="")
        {
            Message = message;
        }

        private string _message;
        public override string Message
        {
            get
            {
                _message = string.IsNullOrEmpty(_message)
                   ? Hardcoded.GetValue("required", Context)
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
            return string.Format("{{ type: \"required\", message: \"{0}\" }}", Message);
        }

        public override bool IsValid(object value)
        {
            if (value == null || (value as string)?.Length == 0)
            {
                return false;
            }

            return true;
        }
    }
}
