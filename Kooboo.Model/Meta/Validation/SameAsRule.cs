using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Validation
{
    public class SameAsRule:ValidationRule
    {
        private string _field;
        public SameAsRule(string field,string message)
        {
            _field = field;
            Message = message;
        }

        public override string GetRule()
        {
            return string.Format("{{type:\"sameAs\",field:\"{1}\",message:\"{0}\"}}", Message,_field);
        }

        public override bool IsValid(object value)
        {
            return false;
        }

    }
}
