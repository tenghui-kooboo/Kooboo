using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Validation
{
    public class MaxLengthRule:ValidationRule
    {
        public int MaxLength;

        public MaxLengthRule(int maxLength,string message)
        {
            MaxLength = maxLength;
            Message = string.Format(message,MaxLength).Replace("\"", "\\\"");
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
