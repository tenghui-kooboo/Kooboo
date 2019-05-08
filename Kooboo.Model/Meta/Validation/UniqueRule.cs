using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Validation
{
    public class UniqueRule : ValidationRule
    {
        public string Api { get; set; }
        public UniqueRule(string api,string message)
        {
            Api = api;
            Message = message;
        }

        public override string GetRule()
        {
            return string.Format("{{type:\"unique\",api:\"{1}\",message:\"{0}\"}}", Message,Api);
        }

        public override bool IsValid(object value)
        {
            return true;
        }
    }
}
