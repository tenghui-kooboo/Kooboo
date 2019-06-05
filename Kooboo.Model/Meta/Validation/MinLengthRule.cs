using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Data.Language;

namespace Kooboo.Model.Meta.Validation
{
    public class MinLengthRule:ValidationRule
    {
        public int MinLength;

        public MinLengthRule(int minLength, string message="")
        {
            MinLength = minLength;
            Message = message;
        }
        private string _message;
        public override string Message
        {
            get
            {
                _message = string.IsNullOrEmpty(_message)
                   ? string.Format(Hardcoded.GetValue("min length is {0}", Context), MinLength)
                    : string.Format(Hardcoded.GetValue(_message, Context), MinLength);

                return _message;
            }
            set
            {
                _message = value;
            }
        }

        public override string Type
        {
            get
            {
                return "minLength";
            }
        }

        public override string GetRule()
        {
            return string.Format("{{type:\"minLength\",minLength:{1},message:\"{0}\"}}", Message, MinLength);
        }

        public override bool IsValid(object value)
        {
            int length=0;
            if (base.IsValid(value))
            {
                return true;
            }
            if (value is string str)
            {
                length = str.Length;
            }
            return length >= MinLength;
        }
    }
}
