using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Validation
{
    public class SameAsRule:ValidationRule
    {
        private string _sameAsField;
        public SameAsRule(string field,string message)
        {
            _sameAsField = field;
            Message = message;
        }

        public override string GetRule()
        {
            return string.Format("{{type:\"sameAs\",field:\"{1}\",message:\"{0}\"}}", Message, _sameAsField);
        }

        public override bool IsValid(object value)
        {
            var type = Model.GetType();
            var properties = type.GetProperties().ToList();

            var sameAsProperty = properties.Find(p => p.Name == _sameAsField);
            var property = properties.Find(p => p.Name == Field);
            if (sameAsProperty == null || property == null)
                return false;
            //if(sameAsProperty.)
            //todo compare
            var sameAsValue = sameAsProperty.GetValue(Model, null);

            return sameAsProperty == value;
        }

    }
}
