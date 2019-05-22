using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Data.Language;

namespace Kooboo.Model.Meta.Validation
{
    public class BetweenRule:ValidationRule
    {
        public int From;
        public int To;
        public BetweenRule(int from ,int to,string message="")
        {
            From = from;
            To = to;
            Message = message;
        }

        private string _message;
        public override string Message
        {
            get
            {
                _message = string.IsNullOrEmpty(_message)
                   ? string.Format(Hardcoded.GetValue("length must be between {0} and {1}", Context), From, To)
                    : string.Format(Hardcoded.GetValue(_message, Context), From, To);
                return _message;
            }
            set
            {
                _message = value;
            }
        }

        public override string GetRule()
        {
            return string.Format("{{type:\"between\",from:{0},to:{1},message:\"{2}\"}}",From,To ,Message);
        }

        public override bool IsValid(object value)
        {
            if (base.IsValid(value))
            {
                return true;
            }
            int i;
            
            if(int.TryParse(value.ToString(), out i))
            {
                return i >= From && i <= To;
            }

            return false;
        }

    }
}
