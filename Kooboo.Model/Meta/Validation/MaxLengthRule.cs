using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Data.Language;

namespace Kooboo.Model.Meta.Validation
{
    public class MaxLengthRule:ValidationRule
    {
        public int MaxLength;

        public MaxLengthRule(int maxLength,string message="")
        {
            MaxLength = maxLength;
            Message = message;
        }
        private string _message;
        public override string Message
        {
            get
            {
                _message = string.IsNullOrEmpty(_message)
                   ? string.Format(Hardcoded.GetValue("max length is {0}", Context), MaxLength)
                    : string.Format(Hardcoded.GetValue(_message, Context), MaxLength);

                return _message;
            }
            set
            {
                _message = value;
            }
        }

        public override string GetRule()
        {
            return string.Format("{{type:\"maxLength\",maxLength:{1},message:\"{0}\"}}", Message,MaxLength);
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

            return length <= MaxLength;
        }
    }
}
