using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Validation
{
    public abstract class ValidationRule : Attribute
    {
        protected ValidationRule(string type)
        {
            Type = type;
        }

        protected ValidationRule()
        {
            var typeName = GetType().Name;
            Type = typeName.Substring(typeName.Length - 4).ToLower();
        }

        public string Type { get; set; }

        public Localizable Message { get; set; }
    }
}
