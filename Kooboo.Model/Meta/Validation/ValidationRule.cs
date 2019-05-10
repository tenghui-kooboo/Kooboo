using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Validation
{
    public class ValidationRule : Attribute
    {
        public object Model { get; set; }

        public string Field { get; set; }

        public virtual string Message { get; set; }

        public virtual string GetRule()
        {
            return "{}";
        }

        public virtual bool IsValid(object value)
        {
            //require attribute to valid value is not empty
            if (value == null || (value as string)?.Length == 0)
            {
                return true;
            }
            return false;
        }
    }
}
