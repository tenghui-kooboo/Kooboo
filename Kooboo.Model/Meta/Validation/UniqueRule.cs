using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Data.Language;

namespace Kooboo.Model.Meta.Validation
{
    public class UniqueRule : ValidationRule
    {
        public string Api { get; set; }
        public UniqueRule(string api,string message="")
        {
            Api = api;
            Message = message;
        }

        private string _message;
        public override string Message
        {
            get
            {
                _message = string.IsNullOrEmpty(_message)
                   ? Hardcoded.GetValue("not unique", Context)
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
            return string.Format("{{type:\"unique\",api:\"{1}\",message:\"{0}\"}}", Message,Api);
        }

        public override bool IsValid(object value)
        {
            return true;
        }
    }
}
